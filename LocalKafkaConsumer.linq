<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Confluent.Kafka</NuGetReference>
  <Namespace>Confluent.Kafka</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
</Query>

class Program
{
	public static void Main(string[] args)
	{
		var bsArr = "localhost:9092";
		bsArr = "b-1.iwatchxkafkacluster.kc9x7j.c1.kafka.us-east-1.amazonaws.com:9094,b-2.iwatchxkafkacluster.kc9x7j.c1.kafka.us-east-1.amazonaws.com:9094,b-3.iwatchxkafkacluster.kc9x7j.c1.kafka.us-east-1.amazonaws.com:9094";

		var conf = new ConsumerConfig
		{
			GroupId = "test-consumer-group",
			BootstrapServers = bsArr,
			// Note: The AutoOffsetReset property determines the start offset in the event
			// there are not yet any committed offsets for the consumer group for the
			// topic/partitions of interest. By default, offsets are committed
			// automatically, so in this example, consumption will only start from the
			// earliest message in the topic 'my-topic' the first time you run the program.
			AutoOffsetReset = AutoOffsetReset.Earliest
		};

		var librdkafkapath = @"C:\Users\312198\AppData\Local\LINQPad\NuGet.FW46\librdkafka.redist\librdkafka.redist.1.3.0\runtimes\win-x64\native\librdkafka.dll";
		Library.Load(librdkafkapath);

		using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
		{
			c.Subscribe("my-topic");

			CancellationTokenSource cts = new CancellationTokenSource();
			Console.CancelKeyPress += (_, e) =>
			{
				e.Cancel = true; // prevent the process from terminating.
				cts.Cancel();
			};

			try
			{
				while (true)
				{
					try
					{
						var cr = c.Consume(cts.Token);
						Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}, Partition: {cr.TopicPartition}'.");
						c.Commit();
						var st = new Random().Next(1000, 5000);
						Console.WriteLine($"Sleeping for {st / 1000} secs");
						Thread.Sleep(st);
					}
					catch (ConsumeException e)
					{
						Console.WriteLine($"Error occured: {e.Error.Reason}");
					}
				}
			}
			catch (OperationCanceledException)
			{
				// Ensure the consumer leaves the group cleanly and final offsets are committed.
				c.Close();
			}
		}
	}
}