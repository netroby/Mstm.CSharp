using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Mstm.Json.Core;
using Mstm.Json.Newtonsoft;
using Mstm.Kafka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Kafka.KafkaNet
{
    public class KafkaNetProvider : IKafkaProvider
    {

        private KafkaOptions _options;
        private BrokerRouter _router;
        private Producer _producer;
        private Consumer _consumer;
        private string _topicName;
        private ISerializeProvider _serializeProvider;

        public KafkaNetProvider(string topic, params string[] urls)
        {
            if (urls == null || urls.Length <= 0)
            {
                throw new ArgumentNullException("urls", "未配置Kafka服务器地址！");
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentNullException("topic", "主题名称不能为空！");
            }

            _topicName = topic;
            _options = new KafkaOptions(ConvertToUrl(urls).ToArray());
            _router = new BrokerRouter(_options);
            _producer = new Producer(_router);
            _consumer = new Consumer(new ConsumerOptions(_topicName, _router));
            _serializeProvider = JsonSerializeProvider.GetProvider();
        }


        #region IConsumer

        public void ReceiveMessage(Action<string> action)
        {
            foreach (var message in _consumer.Consume())
            {
                string msgValue = System.Text.Encoding.UTF8.GetString(message.Value);
                action.Invoke(msgValue);
            }
        }

        #endregion




        #region IProducer

        public void SendMessageAsync(params string[] msgs)
        {
            ValidateMessageIsNotNull(msgs);
            _producer.SendMessageAsync(_topicName, ConvertToMessage(msgs));
        }

        public List<KafkaProduceResponse> SendMessageSync(params string[] msgs)
        {
            ValidateMessageIsNotNull(msgs);
            Task<List<ProduceResponse>> task = _producer.SendMessageAsync(_topicName, ConvertToMessage(msgs));
            task.Wait();
            if (task.Status != TaskStatus.RanToCompletion)
            {
                return null;
            }
            List<KafkaProduceResponse> list = new List<KafkaProduceResponse>();
            task.Result.ForEach(response =>
            {
                list.Add(new KafkaProduceResponse()
                {
                    Error = response.Error,
                    Offset = response.Offset,
                    PartitionId = response.PartitionId,
                    Topic = response.Topic
                });
            });
            return list;
        }


        public void SendObjectAsync<T>(params T[] datas)
        {
            ValidateMessageIsNotNull(datas);
            string[] jsons = SendObject<T>(datas);
            SendMessageAsync(jsons.ToArray());
        }

        public List<KafkaProduceResponse> SendObjectSync<T>(params T[] datas)
        {
            ValidateMessageIsNotNull(datas);
            string[] jsons = SendObject<T>(datas);
            var responses = SendMessageSync(jsons.ToArray());
            return responses;
        }


        public KafkaTopic GetTopic(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentNullException("topic", "主题名称不能为空！");
            }
            var topicTemp = _producer.GetTopic(topic);
            return new KafkaTopic()
            {
                ErrorCode = topicTemp.ErrorCode,
                Name = topicTemp.Name,
                Partitions = ConvertToKafkaPartition(topicTemp.Partitions).ToList()
            };
        }

        #endregion



        #region 私有方法

        private IEnumerable<Uri> ConvertToUrl(IEnumerable<string> urls)
        {
            if (urls == null || urls.Count() == 0) { yield break; }
            foreach (var urlStr in urls)
            {
                yield return new Uri(urlStr);
            }
        }


        private IEnumerable<Message> ConvertToMessage(IEnumerable<string> msgs)
        {
            if (msgs == null || msgs.Count() == 0) { yield break; }
            foreach (var msg in msgs)
            {
                yield return new Message(msg);
            }
        }

        private IEnumerable<KafkaPartition> ConvertToKafkaPartition(IEnumerable<Partition> partitions)
        {
            if (partitions == null || partitions.Count() == 0) { yield break; }
            foreach (var partition in partitions)
            {
                yield return new KafkaPartition()
                {
                    ErrorCode = partition.ErrorCode,
                    Isrs = partition.Isrs,
                    LeaderId = partition.LeaderId,
                    PartitionId = partition.PartitionId,
                    Replicas = partition.Replicas
                };
            }
        }

        private string[] SendObject<T>(T[] datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "发送的数据不能为空！");
            }
            List<string> jsons = new List<string>();
            datas.ToList().ForEach(data =>
            {
                string json = _serializeProvider.SerializeObject<T>(data);
                if (json == null) { throw new Exception("序列化发送数据失败！"); }
                jsons.Add(json);
            });
            return jsons.ToArray();
        }

        private void ValidateMessageIsNotNull(Object msg)
        {
            if (msg == null)
            {
                throw new ArgumentException("发送的消息不能为空！");
            }
        }
        #endregion

    }
}
