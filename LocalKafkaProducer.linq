<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Confluent.Kafka</NuGetReference>
  <NuGetReference>librdkafka.redist</NuGetReference>
  <Namespace>Confluent.Kafka</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

class Program
{
	public static async Task Main(string[] args)
	{
		var bsArr = "localhost:9092";
		bsArr = "b-1.iwatchxkafkacluster.kc9x7j.c1.kafka.us-east-1.amazonaws.com:9094,b-2.iwatchxkafkacluster.kc9x7j.c1.kafka.us-east-1.amazonaws.com:9094,b-3.iwatchxkafkacluster.kc9x7j.c1.kafka.us-east-1.amazonaws.com:9094";
		var librdkafkapath = @"C:\Users\312198\AppData\Local\LINQPad\NuGet.FW46\librdkafka.redist\librdkafka.redist.1.3.0\runtimes\win-x64\native\librdkafka.dll";
		Library.Load(librdkafkapath);
		try
		{
			SendMessages(bsArr);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}

	private static void SendMessages(string bsArr)
	{
		var conf = new ProducerConfig { BootstrapServers = bsArr };

		Action<DeliveryReport<Null, string>> handler = r =>
			Console.WriteLine(!r.Error.IsError
				? $"Delivered message to {r.TopicPartitionOffset}"
				: $"Delivery Error: {r.Error.Reason}");

		using (var p = new ProducerBuilder<Null, string>(conf).Build())
		{
			for (int i = 0; i < 100; ++i)
			{
				p.Produce("my-topic", new Message<Null, string> { Value = "Yenna vilai azhaghe " + i.ToString() }, handler);
			}
			// wait for up to 10 seconds for any inflight messages to be delivered.
			p.Flush(TimeSpan.FromSeconds(10));
		}
	}
}