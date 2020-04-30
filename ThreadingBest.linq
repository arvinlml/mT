<Query Kind="Statements">
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// Buggy
for (int i = 0; i < 4; i++)
{     // WARNING: BUGGY CODE, i has unexpected value
	Task.Factory.StartNew(() => Console.WriteLine($"1st loop: {i}"));
}

// Fixed
for (int i = 0; i < 4; i++)
{
	var tmp = i;
	Task.Factory.StartNew(() => Console.WriteLine($"2nd loop: {tmp}"));
}