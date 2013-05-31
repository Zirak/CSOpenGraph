using HtmlAgilityPack;
using System.Collections.Generic;

namespace OpenGraphParser {

	/// <summary>
	/// A dummy class to hook up Open Graph data into.
	/// </summary>
	public class OpenGraphData : Dictionary<string, string> { }

	/// <summary>
	/// A simple OpenGraph data parser.
	/// </summary>
	public class OpenGraph {
		private string metaXPath = "//meta[contains(@property, \"og\")]";

		/// <summary>
		/// Loads the page indicated by url, and parses the Open Graph
		/// parameters inside.
		/// </summary>
		/// <param name="url">Url to webpage.</param>
		/// <returns>OpenGraph data.</returns>
		public OpenGraphData ParseFromUrl (string url) {
			var web = new HtmlWeb();
			return this.ParseDocument(web.Load(url));
		}

		/// <summary>
		/// Opens the file denoted by path, and parses the Open Graph parameters
		/// inside.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		/// <returns>OpenGraph data.</returns>
		public OpenGraphData ParseFromPath (string path) {
			var doc = new HtmlDocument();
			doc.Load(path);
			return this.ParseDocument(doc);
		}

		/// <summary>
		/// Parses the Open Graph parameters inside the passed html document.
		/// </summary>
		/// <param name="doc">HtmlDocument (duh)</param>
		/// <returns>OpenGraph data.</returns>
		public OpenGraphData ParseDocument (HtmlDocument doc) {
			var ret = new OpenGraphData();

			foreach (var node in this.GetOGNodes(doc)) {
				this.AddDataToResult(node, ret);
			}

			return ret;
		}

		/// <summary>
		/// Parses the key/value pair from a node, and adds them to the result.
		/// </summary>
		/// <param name="node">Node to get info from.</param>
		/// <param name="res">Result object to extract data to.</param>
		private void AddDataToResult (HtmlNode node, OpenGraphData res) {
			var key = ParseKeyFromNode(node);

			if (!res.ContainsKey(key)) {
				res.Add(key, ParseValueFromNode(node));
			}
		}

		private string ParseKeyFromNode (HtmlNode node) {
			return node.Attributes["property"].Value.Substring(3);
		}

		private string ParseValueFromNode (HtmlNode node) {
			//TODO: unescape html entities
			return node.GetAttributeValue("content", string.Empty);
		}

		private IEnumerable<HtmlNode> GetOGNodes (HtmlDocument doc) {
			var metas = doc.DocumentNode.SelectNodes(this.metaXPath);

			foreach (var node in metas) {
				if (this.IsValidOGNode(node)) {
					yield return node;
				}
			}
		}

		private bool IsValidOGNode (HtmlNode node) {
			if (!node.Attributes.Contains("property")) {
				return false;
			}

			return node.Attributes["property"].Value.StartsWith("og:");
		}
	}
}