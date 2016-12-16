import graphx.Node;
import graphx.Edge;
import graphx.NodeOrValue;

class GraphExtensions
{
	 public static function anyEdge(graph:Graph<T>, predicate : Edge<T> -> Bool) {
	    for(edge in graph.edges)
	    	if (predicate(edge)) return true;
	    return false;
	  }
	  public function allEdges(graph:Graph<T>,predicate : Edge<T> -> Bool) {
	    for(edge in graph.edges)
	      if (!predicate(node)) return false;
	    return true;
	  }

	  public function containsEdge(graph:Graph<T>,e : Edge<T>) : Bool {
	    return anyEdge(graph,function(edge) {
	      return equals(edge.from, e.from) && equals(edge.to,e.to);
	    });
	  }

  // public function anyNode(predicate : Node<T> -> Bool) {
  //   for (node in nodes)
  //     if (predicate(node)) return true;
  //   return false;
  // }

  // public function allNodes(predicate : Node<T> -> Bool) {
  //   for (node in nodes)
  //     if (!predicate(node)) return false;
  //   return true;
  // }
}