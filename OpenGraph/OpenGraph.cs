using CsQuery;
using System.Collections.Generic;
using System.Linq;

namespace OpenGraphParser {

	/// <summary>
	/// A dummy class to hook up Open Graph data into.
	/// </summary>
	public class OpenGraphData : Dictionary<string, string> { }

	/// <summary>
	/// A simple OpenGraph data parser.
	/// </summary>
	public class OpenGraph {

		/// <summary>
		/// Loads the page indicated by url, and parses the Open Graph
		/// parameters inside.
		/// </summary>
		/// <param name="url">Url to webpage.</param>
		/// <returns>OpenGraph data.</returns>
		public OpenGraphData ParseFromUrl (string url) {
			return this.ParseDocument(CQ.CreateFromUrl(url));
		}

		/// <summary>
		/// Opens the file denoted by path, and parses the Open Graph parameters
		/// inside.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		/// <returns>OpenGraph data.</returns>
		public OpenGraphData ParseFromPath (string path) {
			return this.ParseDocument(CQ.CreateDocumentFromFile(path));
		}

		/// <summary>
		/// Parses the Open Graph parameters inside the passed html document.
		/// </summary>
		/// <param name="dom">CQ dom.</param>
		/// <returns>OpenGraph data.</returns>
		public OpenGraphData ParseDocument (CQ dom) {
			var ret = new OpenGraphData();

			foreach (var elem in this.GetOGElements(dom)) {
				this.AddDataToResult(elem, ret);
			}

			return ret;
		}

		private IEnumerable<IDomElement> GetOGElements (CQ dom) {
			var metas = dom.Document.GetElementsByTagName("meta");
			return metas.Where(this.IsValidOGNode);
		}

		private bool IsValidOGNode (IDomElement elem) {
			if (!elem.HasAttribute("property")) {
				return false;
			}

			return elem.GetAttribute("property").StartsWith("og:");
		}

		/// <summary>
		/// Parses the key/value pair from a node, and adds them to the result.
		/// </summary>
		/// <param name="elem">Element to get info from.</param>
		/// <param name="res">Result object to extract data to.</param>
		private void AddDataToResult (IDomElement elem, OpenGraphData res) {
			var key = ParseKeyFromNode(elem);

			if (!res.ContainsKey(key)) {
				res.Add(key, ParseValueFromNode(elem));
			}
		}

		private string ParseKeyFromNode (IDomElement elem) {
			return elem.GetAttribute("property").Substring(3);
		}

		private string ParseValueFromNode (IDomElement elem) {
			return elem.GetAttribute("content", string.Empty);
		}
	}
}