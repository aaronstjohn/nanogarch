// using Entitas;

// public class ComponentTypeMap {

 
//     public readonly string[] componentNames;
//     public readonly Type[] componentTypes;
//     public readonly int[] componentIds;

//     public ComponentTypeMap(Type t, string[] componentNames,Type[] componentTypes) 
//     {
//         List<Type> filteredTypes = new List<Type>();
//         List<string> filteredNames = new List<string>();
//         List<int> filteredIds = new List<int>();
//         for(int i=0; i<componentTypes.Length; i++)
//         {
//             if(componentTypes[i].IsAssignableFrom(t))
//             {
//                 filteredTypes.Add(componentTypes[i]);
//                 filteredNames.Add(componentNames[i]);
//                 filteredIds.Add(i);
//             }
//         }
//         this.componentIds = filteredIds.ToArray();
//         this.componentNames = filteredNames.ToArray();
//         this.componentTypes = filteredTypes.ToArray();
//     }
//     public IMatcher CreateMatcher()
//     {
//          var matcher = (Matcher)Matcher.AnyOf(this.componentIds);
//          matcher.componentNames =componentNames;
//          return matcher;
//     }
 
// }

// // public static class ContextExtensions {

// //     // public static ContextComponentTypeMap BuildComponentTypeMap(this Context context, Type t)
// //     // {
// //     //     Type[] componentTypes = context.GetComponentTypes();
// //     //     string[] componentNames = context.GetComponentNames();

// //     //     List<Type> filteredTypes = new List<Type>();
// //     //     List<string> filteredNames = new List<string>();
// //     //     List<int> filteredIds = new List<int>();
// //     //     for(int i=0; i<componentTypes.Length; i++)
// //     //     {
// //     //         if(componentTypes[i].IsAssignableFrom(t))
// //     //         {
// //     //             filteredTypes.Add(componentTypes[i]);
// //     //             filteredNames.Add(componentNames[i]);
// //     //             filteredIds.Add(i);
// //     //         }
// //     //     }
// //     //     return new ContextComponentTypeMap(context.ToString(),
// //     //                                                t,
// //     //                             filteredIds.ToArray(),
// //     //                             filteredNames.ToArray(),
// //     //                             filteredTypes.ToArray())
       

// //     // }
// //     // static readonly string[] _pieces = {
// //     //     Res.Piece0,
// //     //     Res.Piece1,
// //     //     Res.Piece2,
// //     //     Res.Piece3,
// //     //     Res.Piece4,
// //     //     Res.Piece5
// //     // };

// //     // public static Entity CreateRandomPiece(this Context context, int x, int y) {
// //     //     return context.CreateEntity()
// //     //         .IsGameBoardElement(true)
// //     //         .AddPosition(x, y)
// //     //         .IsMovable(true)
// //     //         .IsInteractive(true)
// //     //         .AddAsset(_pieces[Random.Range(0, _pieces.Length)]);
// //     // }

// //     // public static Entity CreateBlocker(this Context context, int x, int y) {
// //     //     return context.CreateEntity()
// //     //         .IsGameBoardElement(true)
// //     //         .AddPosition(x, y)
// //     //         .AddAsset(Res.Blocker);
// //     // }
// // }
