import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;
import 'globals.dart';

//*************************************************************************************** 
//Order.dart 
//Extends from the home page after user has logged into the app  
//*************************************************************************************** 

//Fetch post o retrieve the endpoint from the rest api 
Future< OrdersList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/orders'); //JSON format endpoint 
  
  if (response.statusCode == 200)
  {
    return OrdersList.fromJson( json.decode( response.body ) ); //decoding the endpoint 
  } 
  else 
  {
    throw Exception( 'Failed to load post' ); //error message if the endpoint cannot be retrived and decoded 
  }
}//END of fetchpost 


//Class for the Orders list 
class OrdersList 
{
  final List< OrdersPost > posts;

  OrdersList( { this.posts } );

  factory OrdersList.fromJson( List< dynamic > parsedJson )
  {
    List< OrdersPost > orders = new List< OrdersPost >();
    orders = parsedJson.map( ( i ) => OrdersPost.fromJson( i ) ).toList();

    return OrdersList( posts: orders );
  }
}//END of OrdersList class 


//Orders Post class 
//Each post gets the corresponding parsed JSON attribute 
class OrdersPost
{
  //attributes of each of the Orders 
  final int    orderID;
  final String orderDate;
  final String requiredDate;
  final String shippedDate;
  final String shippedName;
  final String shippedAddress;
  final String shippedCity;
  final String shippedRegion;
  final double freightNumber;
  final String userName;

  OrdersPost( 
    { 
          this.orderID,
          this.orderDate,
          this.requiredDate,
          this.shippedDate,
          this.shippedName,
          this.shippedAddress,
          this.shippedCity,
          this.shippedRegion,
          this.freightNumber,
          this.userName
    });

  factory OrdersPost.fromJson (Map< String, dynamic > json )
  {
    return OrdersPost(
      orderID:        json[ 'OrderID' ],
      orderDate:      json[ 'OrderDate' ],
      requiredDate:   json[ 'RequiredDate' ],
      shippedDate:    json[ 'ShippedDate' ],
      shippedName:    json[ 'ShippedName' ],
      shippedAddress: json[ 'ShippedAddress' ],
      shippedCity:    json[ 'ShippedCity' ],
      shippedRegion:  json[ 'ShippedRegion' ],
      freightNumber:  json[ 'Freight' ],
      userName:       json[ 'UserName' ]
    );
  }
}//END of the OrderPost class 


//Order Class that contains all of attributes for each order 
class Order
{
  final int    orderID;
  final String orderDate;
  final String requiredDate;
  final String shippedDate;
  final String shippedName;
  final String shippedAddress;
  final String shippedCity;
  final String shippedRegion;
  final double freightNumber;
  final String userName;

  Order({
      this.orderID,
      this.orderDate,
      this.requiredDate,
      this.shippedDate,
      this.shippedName,
      this.shippedAddress,
      this.shippedCity,
      this.shippedRegion,
      this.freightNumber,
      this.userName
  });
}//END of the Order class 


//Instantiating an Order widget 
class OrdersWidg extends StatefulWidget 
{
  OrdersWidg({Key key}) : super(key: key);
  @override
  _OrdersWidgState createState() => new _OrdersWidgState();
}

//Full order Widget that will allow the user to view the orders 
class _OrdersWidgState extends State< OrdersWidg > 
{
  Widget appBarTitle = new Text(
    "Recent Orders",
    style: Globals.textStyle,
  );
  
  final key = new GlobalKey<ScaffoldState>();
  final TextEditingController _searchQuery = new TextEditingController();
  List< Order > _list;
  bool _IsSearching;
  String _searchText = "";

  _SearchListState() {
    _searchQuery.addListener(() {
      if (_searchQuery.text.isEmpty) {
        setState(() {
          _IsSearching = false;
          _searchText = "";
        });
      } else {
        setState(() {
          _IsSearching = true;
          _searchText = _searchQuery.text;
        });
      }
    });
  }

  //Construction of the widget and its internals below 
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: Globals.backgroundColor,
      key: key,
      appBar: AppBar(
        centerTitle: true,
        title: Text( 'Recent Orders', style: Globals.textStyle ), 
      ),
      body: FutureBuilder< OrdersList >(
        future: fetchPost(),
        builder: ( context, snapshot ) {
          if ( snapshot.hasData )
          {
            _list = List();
            Order order;

            for ( int i = 0; i < snapshot.data.posts.length; i++ ) //loop goes through each part of each post and adds them to a string 
            {
              order = Order(
                  orderID:        snapshot.data.posts[ i ].orderID,
                  orderDate:      snapshot.data.posts[ i ].orderDate.toString(),
                  requiredDate:   snapshot.data.posts[ i ].requiredDate.toString(),
                  shippedDate:    snapshot.data.posts[ i ].shippedDate.toString(),
                  shippedAddress: snapshot.data.posts[ i ].shippedAddress.toString(),
                  shippedCity:    snapshot.data.posts[ i ].shippedCity.toString(),
                  shippedRegion:  snapshot.data.posts[ i ].shippedRegion.toString(),
                  shippedName:    snapshot.data.posts[ i ].shippedName.toString(),
                  freightNumber:  snapshot.data.posts[ i ].freightNumber,
                  userName:       snapshot.data.posts[ i ].userName.toString()
              );

              _list.add( order ); //adding each order to the list 
            }

            return ListView.builder(
              itemCount: _list.length,
              itemBuilder: ( context, index ) {
                return Card(
                  elevation: 8.0,
                  margin: new EdgeInsets.symmetric( horizontal: 10.0, vertical: 10.0 ),
                  child: Container(
                    decoration: BoxDecoration( 
                      color: Globals.barColor
                    ),
                    child: ListTile(
                      contentPadding: EdgeInsets.symmetric( horizontal: 10.0, vertical: 6.0 ),
                      title: Row(
                        children: <Widget>[
                          Text( 
                            'Order ID:         ' + '\n' +
                            'Order Date:       ' + '\n' +
                            'Required Date:    ' + '\n' +
                            'Shipped Date:     ' + '\n' +
                            'Shipped Address:  ' + '\n' +
                            'Shipped City:     ' + '\n' + 
                            'Shipped Region:   ' + '\n' + 
                            'Shipped Name:     ' + '\n' +
                            'Freight:          ' + '\n' + 
                            'User:             ',
                            style: Globals.textStyle
                          ),
                          Text( 
                            _list.elementAt( index ).orderID.toString()        + '\n' +
                            _list.elementAt( index ).orderDate.toString()      + '\n' +
                            _list.elementAt( index ).requiredDate.toString()   + '\n' +
                            _list.elementAt( index ).shippedDate.toString()    + '\n' +
                            _list.elementAt( index ).shippedAddress.toString() + '\n' + //Formatting the text 
                            _list.elementAt( index ).shippedCity.toString()    + '\n' +
                            _list.elementAt( index ).shippedRegion.toString()  + '\n' +
                            _list.elementAt( index ).shippedName.toString()    + '\n' +
                            _list.elementAt( index ).freightNumber.toString()  + '\n' +
                            _list.elementAt( index ).userName.toString(),
                            style: Globals.textStyle
                          )
                        ],
                      )
                    )
                  )
                );
              }
            );
          }

          else
          {
            return Text( 'Loading...' );
          }
        }
      )
    );
  }//END of Order widget  
  //END OF WORKING CODE  
  
  
  

//   Widget buildBar(BuildContext context) {
//     return new AppBar(centerTitle: true, title: appBarTitle, actions: <Widget>[
//       new IconButton(
//         icon: actionIcon,
//         onPressed: () {
//           setState(() {
//             if (this.actionIcon.icon == Icons.search) {
//               this.actionIcon = new Icon(
//                 Icons.close,
//                 color: Colors.white,
//               );
//               this.appBarTitle = new TextField(
//                 controller: _searchQuery,
//                 style: new TextStyle(
//                   color: Colors.white,
//                 ),
//                 decoration: new InputDecoration(
//                     prefixIcon: new Icon(Icons.search, color: Colors.white),
//                     hintText: "Search...",
//                     hintStyle: new TextStyle(color: Colors.white)),
//               );
//               _handleSearchStart();
//             } else {
//               _handleSearchEnd();
//             }
//           });
//         },
//       ),
//     ]);
//   }

//   void _handleSearchStart() {
//     setState(() {
//       _IsSearching = true;
//     });
//   }

//   void _handleSearchEnd() {
//     setState(() {
//       this.actionIcon = new Icon(
//         Icons.search,
//         color: Colors.white,
//       );
//       this.appBarTitle = new Text(
//         "Search Sample",
//         style: new TextStyle(color: Colors.white),
//       );
//       _IsSearching = false;
//       _searchQuery.clear();
//     });
//   }
// }

// class ChildItem extends StatelessWidget {
//   final String name;
//   ChildItem(this.name);
//   @override
//   Widget build(BuildContext context) {
//     return new ListTile(title: new Text(this.name));
//   }
}
