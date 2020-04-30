<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>iWatch.TIBCO.EMS</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>TIBCO.EMS</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

public class csBrowser
{
	public static void Main(string[] args)
	{
		string serverUrl = "tcp://10.17.193.24:7200";
		serverUrl = "tcp://10.45.16.217:7600";
		// "TRANSFEED" - uName = retval.Pwd = "RTRACRTCASE";
		// GSI "IWATCHCASESVC" retval.uName = retval.Pwd = "RTRA2IWATCHCASE";
		// Case 			retval.uName = retval.Pwd = "IWATCHCASESVC";
		string userName = "IWATCHCASESVC";
		string password = "IWATCHCASESVC";
		userName = "unisysUser";
		password = "82#sg5$@Dy1s";
		string env = "QA";
		string queueName = $"WU.{env}.IWATCH.COMPLIANCE.LPMT.QUEUE";
		queueName = "WU.UAT.UNISYS.PAYOUTENGINE.RELEASE.PAYOUT.QUEUE";
		csBrowser t = new csBrowser(serverUrl, userName, password, queueName);
	}
	
	public csBrowser(string serverUrl, string userName, string password,string queueName)
	{
		Console.WriteLine("\n------------------------------------------------------------------------");
		Console.WriteLine("Browse Queue: "+queueName);
		Console.WriteLine("------------------------------------------------------------------------");
		Console.WriteLine("Server....................... " + ((serverUrl != null) ? serverUrl : "localhost"));
		Console.WriteLine("User......................... " + ((userName != null) ? userName : "(null)"));
		Console.WriteLine("Queue........................ " + queueName);
		Console.WriteLine("------------------------------------------------------------------------\n");
		try
		{
			int message_number=0;
			ConnectionFactory factory = new ConnectionFactory(serverUrl);

			Connection connection = factory.CreateConnection(userName, password);
			Session session = connection.CreateSession(false, Session.EXPLICIT_CLIENT_ACKNOWLEDGE);
			TIBCO.EMS.Queue queue = session.CreateQueue(queueName);
			MessageProducer producer = session.CreateProducer(queue);
			Message message = null;

			connection.Start();

			// create browser and browse what is there in the queue
			Console.WriteLine("--- Browsing the queue.");

			QueueBrowser browser = session.CreateBrowser(queue);
			
			IEnumerator msgs = browser.GetEnumerator();

			int browseCount = 0;

			while (msgs.MoveNext())
			{
				message = (Message)msgs.Current;
				Console.WriteLine($"Browsed message {++browseCount} : " + message.MessageID);
			}

			Console.WriteLine("--- No more messages in the queue.");

			// try to browse again, if no success for some time then quit
			// notice that we will NOT receive new messages instantly. It
			// happens because QueueBrowser limits the frequency of query
			// requests sent into the queue after the queue was
			// empty. Internal engine only queries the queue every so many
			// seconds, so we'll likely have to wait here for some time.

			int attemptCount = 0;
			while (!msgs.MoveNext())
			{
				attemptCount++;
				Console.Write("Waiting for messages to arrive, count=" + attemptCount );
				Thread.Sleep(5000);
				if (attemptCount > 30)
				{
					Console.Write("Still no messages in the queue after " + attemptCount + " seconds");
					Environment.Exit(0);
				}
			}

			// got more messages, continue browsing
			Console.WriteLine("Found more messages. Continue browsing.");
			do
			{
				message = (Message)msgs.Current;
				Console.WriteLine($"Browsed message {++browseCount} : " + message.MessageID);
			} while (msgs.MoveNext());

			// close all and quit
			browser.Close();

			connection.Close();
		}
		catch (EMSException e)
		{
			Console.Error.WriteLine("Exception in csBrowser: " + e.Message);
			Console.Error.WriteLine(e.StackTrace);
			Environment.Exit(0);
		}
		catch (ThreadInterruptedException e)
		{
			Console.Error.WriteLine("Exception in csBrowser: " + e.Message);
			Console.Error.WriteLine(e.StackTrace);
			Environment.Exit(0);
		}

	}

}