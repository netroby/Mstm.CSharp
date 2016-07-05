using Mstm.Kafka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mstm.Kafka.KafkaNet.Tests
{
    public class KafkaNetProviderTests
    {
        private IKafkaProvider _provider;

        public KafkaNetProviderTests()
        {
            _provider = new KafkaNetProvider("TestForMstm2", "http://192.168.233.129:9092");
        }


        [Fact]
        public void SendMessageTest_Success()
        {
            _provider.SendMessage("Hello Test");
        }

        [Fact]
        public void ReceiveMessageTest_Success()
        {
            _provider.ReceiveMessage((msg) =>
            {
                Console.WriteLine(msg);
            });
        }
    }
}
