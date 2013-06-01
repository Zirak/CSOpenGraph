﻿using OpenGraphParser;
using System;

namespace OGTest {

	internal class Program {

		private static void Main (string[] args) {
			string url = "http://chat.stackoverflow.com/rooms/17/javascript";
			var data = new OpenGraph().ParseFromUrl(url);

			foreach (var pair in data) {
				Console.WriteLine("{0} => {1}", pair.Key, pair.Value);
			}
		}
	}
}
