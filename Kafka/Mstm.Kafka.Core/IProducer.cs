using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Kafka.Core
{

    /// <summary>
    /// 生产者接口
    /// </summary>
    public interface IProducer
    {

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgs">要发送的消息集合</param>
        void SendMessage(params string[] msgs);

        /// <summary>
        /// 获取主题信息
        /// </summary>
        /// <param name="topic">主题名称</param>
        /// <returns>主题对象</returns>
        KafkaTopic GetTopic(string topic);
    }
}
