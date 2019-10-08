﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DFAGraph
{
    public class DFANode : MonoBehaviour
    {
        private bool _visited;
        public bool Visited {
            get {
                return _visited;
            }
            set {
                _visited = value;
            }
        }
        public bool _isCurrent;
        private Color _lastColor;
        public bool IsCurrent
        {
            get
            {
                return _isCurrent;
            }
            set
            {
                _isCurrent = value;
                if (value)
                {
                    _lastColor = gameObject.GetComponent<Image>().color;
                    gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 1f);
                }
                else
                {
                    gameObject.GetComponent<Image>().color = _lastColor;
                }
            }
        }
        public DFANode shortestPrevious = null;
        public List<DFAEdge> edges = new List<DFAEdge>();

        //When you cut a wire, call currentPosition.Traverse(wire)
        public DFANode NextNode(string wireColor)
        {
            string outputStr = "Options: \n";
            foreach (DFAEdge edge in edges)
            {
                outputStr += "  Color: " + edge.GetColorStr() + "\n";
                outputStr += "  Child Visited: " + edge.child.Visited + "\n";
                outputStr += "  Child Edges:\n";

                foreach (DFAEdge ce in edge.child.edges)
                {
                    outputStr += "    Color: " + ce.GetColorStr() + "\n";
                    outputStr += "    Child Visited: " + ce.child.Visited + "\n";
                }
            }
            Debug.Log(outputStr);

            Visited = true;
            foreach (DFAEdge edge in edges)
            {
                if (edge.GetColorStr() == wireColor && !edge.child.Visited)
                    return edge.child;
            }
            //Don't know if we want an error state node or just return null
            return null;
        }

        public bool HasEdge(string strColor)
        {
            foreach (DFAEdge edge in edges)
            {
                if (edge.GetColorStr() == strColor)
                    return true;
            }
            return false;
        }

        public IEnumerable<DFANode> GetChildNodes()
        {
            return edges.Select(x => x.child);
        }
    }
}

