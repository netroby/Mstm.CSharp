using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
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

        public void SendMessage(params string[] msgs)
        {
            _producer.SendMessageAsync(_topicName, ConvertToMessage(msgs));
        }


        #endregion



        #region 私有方法
        private IEnumerable<Uri> ConvertToUrl(string[] urls)
        {
            foreach (var urlStr in urls)
            {
                yield return new Uri(urlStr);
            }
        }


        private IEnumerable<Message> ConvertToMessage(IEnumerable<string> msgs)
        {
            foreach (var msg in msgs)
            {
                yield return new Message(msg);
            }
        }
        #endregion




        public KafkaTopic GetTopic(string topic)
        {
            throw new NotImplementedException();
        }
    }
}
