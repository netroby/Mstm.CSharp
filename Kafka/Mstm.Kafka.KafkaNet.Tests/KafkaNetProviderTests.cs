using Mstm.Kafka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Mstm.Kafka.KafkaNet.Tests
{
    public class KafkaNetProviderTests
    {
        private IKafkaProvider _provider;

        public KafkaNetProviderTests()
        {
            _provider = new KafkaNetProvider("TestForMstm", "http://192.168.233.129:9092");
        }


        /// <summary>
        /// 发送消息测试
        /// </summary>
        [Fact]
        public void SendMessageTest_Success()
        {
            _provider.SendMessage("Hello Test");
        }


        /// <summary>
        /// 接收消息测试
        /// </summary>
        [Fact]
        public void ReceiveMessageTest_Success()
        {
            var task = Task.Run(() =>
             {
                 _provider.ReceiveMessage((msg) =>
                 {
                     Console.WriteLine(msg);
                 });
             });

            task.Wait(1000 * 10);
        }


        /// <summary>
        /// 获取主题测试
        /// </summary>
        [Fact]
        public void GetTopicTest_Success()
        {
            string topicName = "TestForMstm";
            KafkaTopic topic = _provider.GetTopic(topicName);
            topic.ShouldNotBeNull();
        }
    }
}
