using HtmlAgilityPack;
using System.Collections.Generic;

namespace OpenGraphParser {

	public class OpenGraphData : Dictionary<string, string> { }

	public class OpenGraph {
		private string metaXPath = "//meta[contains(@property, \"og\")]";

		public OpenGraphData ParseFromUrl (string url) {
			var web = new HtmlWeb();
			return this.ParseDocument(web.Load(url));
		}

		public OpenGraphData ParseFromPath (string path) {
			var doc = new HtmlDocument();
			doc.Load(path);
			return this.ParseDocument(doc);
		}

		public OpenGraphData ParseDocument (HtmlDocument doc) {
			var ret = new OpenGraphData();

			foreach (var node in this.GetOGNodes(doc)) {
				this.AddDataToResult(node, ret);
			}

			return ret;
		}

		private void AddDataToResult (HtmlNode node, OpenGraphData res) {
			var key = ParseKeyFromNode(node);

			if (!res.ContainsKey(key)) {
				res.Add(key, ParseValueFromNode(node));
			}
		}

		private string ParseKeyFromNode (HtmlNode node) {
			return node.Attributes["property"].Value.Replace("og:", "");
		}

		private string ParseValueFromNode (HtmlNode node) {
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