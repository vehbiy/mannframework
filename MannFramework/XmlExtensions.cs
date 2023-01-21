using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MannFramework
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Creates (does not append) an instance of XMLNode 
        /// </summary>
        public static XmlNode CreateNode(this XmlDocument Document, string Name)
        {
            return Document.CreateNode(XmlNodeType.Element, Name, String.Empty);
        }

        /// <summary>
        /// Creates (does not append) an instance of XMLNode - xmltools
        /// </summary>
        public static XmlNode CreateNode(this XmlDocument Document, string Name, string Prefix, string NamespaceUri)
        {
            return Document.CreateNode(XmlNodeType.Element, Prefix, Name, NamespaceUri);
        }

        /// <summary>
        /// Appends Node with specified Name to the root
        /// </summary>
        /// <param name="Name">Name of the Node</param>
        public static XmlNode AppendNode(this XmlDocument Document, string Name)
        {
            return Document.AppendChild(Document.CreateNode(XmlNodeType.Element, Name, String.Empty));
        }

        /// <summary>
        /// Appends node with specified Name under the Parent Node
        /// </summary>
        public static XmlNode AppendNode(this XmlNode Node, string Name)
        {
            return Node.AppendChild(Node.OwnerDocument.CreateNode(XmlNodeType.Element, Name, String.Empty));
        }

        public static XmlNode AppendNode(this XmlNode ParentNode, string Name, string InnerText)
        {
            XmlNode childnode = ParentNode.OwnerDocument.CreateNode(XmlNodeType.Element, Name, String.Empty);
            childnode.InnerText = InnerText;
            return ParentNode.AppendChild(childnode);
        }

        /// <summary>
        /// Appends Node with specified Name to the root - xmlhelper
        /// </summary>
        /// <param name="Name">Name of the Node</param>
        public static XmlNode AppendNode(this XmlDocument Document, string Name, string Prefix, string NamespaceUri)
        {
            return Document.AppendChild(Document.CreateNode(XmlNodeType.Element, Prefix, Name, NamespaceUri));
        }

        /// <summary>
        /// Appends node with specified Name under the Parent Node - xmlhelper
        /// </summary>
        public static XmlNode AppendNode(this XmlNode Node, string Name, string Prefix, string NamespaceUri)
        {
            return Node.AppendChild(Node.OwnerDocument.CreateNode(XmlNodeType.Element, Prefix, Name, NamespaceUri));
        }

        /// <summary>
        /// - xmlhelper
        /// </summary>
        /// <param name="ParentNode"></param>
        /// <param name="Name"></param>
        /// <param name="InnerText"></param>
        /// <param name="Prefix"></param>
        /// <param name="NamespaceUri"></param>
        /// <returns></returns>
        public static XmlNode AppendNode(this XmlNode ParentNode, string Name, string InnerText, string Prefix, string NamespaceUri)
        {
            XmlNode childnode = ParentNode.OwnerDocument.CreateNode(XmlNodeType.Element, Prefix, Name, NamespaceUri);
            childnode.InnerText = InnerText;
            return ParentNode.AppendChild(childnode);
        }

        /// <param name="Node">If it is null, attribute will be append to the root node</param>
        public static XmlAttribute AppendAttribute(this XmlNode Node, string Name, string Value)
        {
            //if (Node == null)
            //    Node = Document.FirstChild;
            XmlAttribute attribute = Node.OwnerDocument.CreateAttribute(Name);
            attribute.Value = Value;
            return Node.Attributes.Append(attribute);
        }

        public static XmlAttribute AppendAttribute(this XmlNode Node, string Prefix, string Name, string Value)
        {
            //if (Node == null)
            //    Node = Document.FirstChild;
            XmlAttribute attribute = Node.OwnerDocument.CreateAttribute(Prefix, Name, "http://www.w3.org/2001/XMLSchema-instance");
            attribute.Value = Value;
            return Node.Attributes.Append(attribute);
        }

        public static bool AreChildrenValid(this XmlNode Node)
        {
            return Node != null && Node.ChildNodes != null && Node.ChildNodes.Count != 0;
        }

        public static T GetChildNodeValue<T>(this XmlNode Node, string ChildNodeName)
        {
            string value = GetChildNodeValue(Node, ChildNodeName);
            return Helpers.GetValueFromObject<T>(value);
        }

        public static string GetChildNodeValue(this XmlNode Node, string ChildNodeName, XmlNamespaceManager Manager)
        {
            if (Node == null)
            {
                return null;
            }

            XmlNode ChildNode = Node.SelectSingleNode(ChildNodeName, Manager);

            if (ChildNode == null)
            {
                return null;
            }

            return ChildNode.InnerText;
        }

        public static string GetChildNodeValue(this XmlNode Node, string ChildNodeName)
        {
            return GetChildNodeValue(Node, ChildNodeName);
        }

        public static string GetChildNodeValue(this XmlNode Node, string ChildNodeName, string Namespace)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(Node.OwnerDocument.NameTable);
            nsmgr.AddNamespace("a", Namespace);
            return Node.GetChildNodeValue("a:" + ChildNodeName, nsmgr);
        }

        public static T GetChildNodeValue<T>(this XmlNode Node, string ChildNodeName, string Namespace)
        {
            string value = GetChildNodeValue(Node, ChildNodeName, Namespace);
            return Helpers.GetValueFromObject<T>(value);
        }

        public static XmlNodeList SelectNodes(this XmlNode Node, string XPath, string Namespace)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(Node.OwnerDocument.NameTable);
            nsmgr.AddNamespace("a", Namespace);
            return Node.SelectNodes("a:" + XPath, nsmgr);
        }

        public static XmlNode SelectSingleNode(this XmlNode Node, string XPath, string Namespace)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(Node.OwnerDocument.NameTable);
            nsmgr.AddNamespace("a", Namespace);
            return Node.SelectSingleNode("a:" + XPath, nsmgr);
        }

        public static string GetAttributeValue(this XmlNode Node, string AttributeName)
        {
            if (Node == null)
                return null;

            XmlAttribute attribute = Node.Attributes[AttributeName];

            if (attribute == null)
                return null;

            return attribute.Value;
        }

        public static T GetAttributeValue<T>(this XmlNode Node, string AttributeName)
        {
            string value = GetAttributeValue(Node, AttributeName);
            return Helpers.GetValueFromObject<T>(value);
        }
    }
}
