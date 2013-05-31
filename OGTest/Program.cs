using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGraphParser;

namespace OGTest {
	class Program {
		static void Main (string[] args) {
			var data = new OpenGraph().ParseFromPath(@"..\..\test.html");

			foreach (var pair in data) {
				Console.WriteLine(@"{0} => {1}", pair.Key, pair.Value);
			}
		}
	}
}
