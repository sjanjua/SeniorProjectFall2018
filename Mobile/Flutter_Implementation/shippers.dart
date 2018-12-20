import 'package:flutter/material.dart';
import 'home.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;
import 'globals.dart';

//************************************************************************************* 
//Shipper page to view the Shippers that are moving the inventory 
//One of the functionalities that is accessible from the home page once
//a user has successfully logged into the app 
//************************************************************************************* 

//Fetch the endpoint from the rest api to be used in the app 
Future< ShippersList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/shippers'); //endpoint for shippers 
  
  if (response.statusCode == 200)
  {
    return ShippersList.fromJson( json.decode( response.body ) );
  } 
  else 
  {
    throw Exception('Failed to load post'); //error message if the endpoint cannot be reached 
  }
}

//Shippers List Class that parses the JSON so that it can be manipulated inside the app 
class ShippersList
{
  final List< ShippersPost > posts;

  ShippersList( { this.posts } );

  //List parses the JSON and adds the shippers to a list 
  factory ShippersList.fromJson( List< dynamic > parsedJson ) 
  {
    List< ShippersPost > shippers = new List< ShippersPost >();
    shippers = parsedJson.map( ( i ) => ShippersPost.fromJson( i ) ).toList();

    return ShippersList( posts: shippers );
  }
}

//Class for the shipper posts that contains the attributes that each shipper post has 
//Each shipper has--> an ID, a Name, and  Phone number  
//Class takes each parsed JSON value and assigns it to the appropriate variable in each post 
class ShippersPost
{
  int    shipperID;
  String shipperName;
  String shipperPhone;

  ShippersPost( { this.shipperID, this.shipperName, this.shipperPhone } );

  factory ShippersPost.fromJson( Map< String, dynamic > parsedJson )
  {
    return ShippersPost(
      shipperID:    parsedJson[ 'ShipperID' ],
      shipperName:  parsedJson[ 'ShipperName' ],
      shipperPhone: parsedJson[ 'Phone' ]
    );
  }
}

//Instantiate a Shipper widget 
class ShippersWidg extends StatefulWidget
{
  ShippersWidg({Key key}) : super(key: key);

  @override
  _ShippersWidgState createState() => new _ShippersWidgState();
}

//Setting up widget to search for shippers 
class _ShippersWidgState extends State< ShippersWidg > 
{
  Widget appBarTitle = new Text(
    "Search For Shippers",
    style: Globals.textStyle,
  );
  Icon actionIcon = new Icon(
    Icons.search,
    color: Colors.white,
  );
  
  //Shipper searching mechanics 
  final key = new GlobalKey<ScaffoldState>();
  final TextEditingController _searchQuery = new TextEditingController();
  List< ShippersPost > _list;
  List< ChildItem > _childViews;
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

  //Building the shippers Widget displaying the shippers that were searched for 
  //outputs the shippers in a list 
  @override
  Widget build(BuildContext context) {

    return new Scaffold(
      backgroundColor: Globals.backgroundColor,
      key: key,
      appBar: AppBar(
            centerTitle: true,
            title: Text( 'Shippers', style: Globals.textStyle ), 
          ),
      body: FutureBuilder< ShippersList >(
        future: fetchPost(),
        builder: ( context, snapshot ) {
          if ( snapshot.hasData )
          {

            _list = List();
            ShippersPost post;

            for ( int i = 0; i < snapshot.data.posts.length; i++ )
            {
              int    shipperID     = snapshot.data.posts[ i ].shipperID;
              String shipperName   = snapshot.data.posts[ i ].shipperName.toString();
              String shipperPhone  = snapshot.data.posts[ i ].shipperPhone.toString();

              post = ShippersPost(
                shipperID: shipperID,
                shipperName: shipperName,
                shipperPhone: shipperPhone
              );
              _list.add( post );
            }

            return ListView.builder(
              itemCount: _list.length,
              itemBuilder: ( context, index ) {
                return Card(
                  elevation: 8.0,
                  margin: EdgeInsets.symmetric( horizontal: 10.0, vertical: 10.0 ),
                  child: Container(
                    decoration: BoxDecoration( color: Globals.barColor ),
                    child: ListTile(
                      contentPadding: EdgeInsets.symmetric( horizontal: 10.0, vertical: 6.0 ),
                      title: Text( 
                        _list[ index ].shipperName + '\n' + '\n' +
                        _list[ index ].shipperPhone, 
                      style: Globals.textStyle, 
                      textAlign: TextAlign.center )
                    ),
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
  }
// _IsSearching ? _buildSearchList() : _buildList()
  // List<ChildItem> _buildList()
  // {
  //   return _list.map((contact) => new ChildItem(contact)).toList();
  // }

  // List<ChildItem> _buildSearchList() 
  // {
  //   if (_searchText.isEmpty) {
  //     return _list.map((contact) => new ChildItem(contact)).toList();
  //   } else {
  //     List<String> _searchList = List();
  //     for (int i = 0; i < _list.length; i++) {
  //       String name = _list.elementAt(i);
  //       if (name.toLowerCase().contains(_searchText.toLowerCase())) {
  //         _searchList.add(name);
  //       }
  //     }
  //     return _searchList.map((contact) => new ChildItem(contact)).toList();
  //   }
  }

  // Widget buildBar(BuildContext context) {
  //   return new AppBar(centerTitle: true, title: appBarTitle, actions: <Widget>[
  //     new IconButton(
  //       icon: actionIcon,
  //       onPressed: () {
  //         setState(() {
  //           if (this.actionIcon.icon == Icons.search) {
  //             this.actionIcon = new Icon(
  //               Icons.close,
  //               color: Colors.white,
  //             );
  //             this.appBarTitle = new TextField(
  //               controller: _searchQuery,
  //               style: new TextStyle(
  //                 color: Colors.white,
  //               ),
  //               decoration: new InputDecoration(
  //                   prefixIcon: new Icon(Icons.search, color: Colors.white),
  //                   hintText: "Search...",
  //                   hintStyle: new TextStyle(color: Colors.white)),
  //             );
  //             _handleSearchStart();
  //           } else {
  //             _handleSearchEnd();
  //           }
  //         });
  //       },
  //     ),
  //   ]);
  // }

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

//Child item class for building the shipper widget 
class ChildItem extends StatelessWidget {
  final String name;
  ChildItem(this.name);
  @override
  Widget build(BuildContext context) {
    return new ListTile(title: new Text(this.name));
  }
}
