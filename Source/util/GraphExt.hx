package util;
import graphx.Node;
import graphx.Edge;
import graphx.NodeOrValue;
import graphx.Graph;
import Type;
class GraphExt<T> extends Graph<T>
{
	 public  function anyEdge( predicate : Edge<T> -> Bool) {
	    for(edge in edges)
	    	if (predicate(edge)) return true;
	    return false;
	  }
	  public function allEdges(predicate : Edge<T> -> Bool) {
	    for(edge in edges)
	      if (!predicate(edge)) return false;
	    return true;
	  }
	  // public function edgeEquals(e1:Edge<T>,e2:Edge<T>) : Bool 
	  // {
	  // 	 	return e1.to.equals(e2.to) && e1.from.equals(e2.from);
	  // }
	  public function containsEdge(from : NodeOrValue<T>, to : NodeOrValue<T>) : Bool {
	    return anyEdge(function(edge) {
	      return nodeFunctions.isEqual(from.toValue(), edge.from.value) && nodeFunctions.isEqual(to.toValue(),edge.to.value);
	    });
	  }


}