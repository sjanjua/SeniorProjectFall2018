import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

Future<Shippers> fetchPost() async
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/shippers');

  if ( response.statusCode == 200 )
  {   
    return Shippers.fromJson( json.decode( response.body ) );
  }
  else
  {
    throw Exception('Failed to load post');
  }
}

class Shippers
{
  final List< Post > posts;

  Shippers( {this.posts} );

  factory Shippers.fromJson( Map< String, dynamic > parsedJson )
  {
    var list = parsedJson[ 'Shippers' ] as List;
    List< Post > postList = list.map( ( i ) => Post.fromJson( i ) ).toList();

    return Shippers(
      posts: postList
    );
  } 
} 

/*class Supplier 
{
  final List <Post> posts; 

  Supplier({this.post}); 

  factory Supplier.fromJson( Map < String, dynamic > parsedJson)
  {
    var list = parsedJson[]
  }
}
*/ 

class Post
{
  final int    shipperID;
  final String companyName;
  //final String phone;

  Post({this.shipperID, this.companyName});

  factory Post.fromJson( Map< String, dynamic > json )
  {
    return Post(
      shipperID: json[ 'ShipperID' ],
      companyName: json[ 'ShipperName' ],
      //phone: json[ 'Shipper Phone' ]
    );
  }
}

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Inventory Management System',
      theme: ThemeData(
        primarySwatch: Colors.blueGrey,
      ),
      home: Scaffold(
        appBar: AppBar( 
          title: Text('Fetch Data Example'),
          actions: <Widget>[
            IconButton(
              icon: Icon( Icons.shopping_cart ),
              onPressed: () {}
            ),
            IconButton( 
              icon: Icon( Icons.monetization_on ),
              onPressed: () {}
            )
          ],
        ),
        drawer: Drawer(
          child: ListView(
            children: <Widget>[
              UserAccountsDrawerHeader(
                accountName: Text( "User Name" ),
                accountEmail: Text( "SampleEmail@email.com" )
              ),
              ListTile(
                title: Text( "Sample Tile 1" ),
                trailing: Icon( Icons.android )
               ),
              ListTile(
                title: Text( "Sample Tile 2" ),
                trailing: Icon( Icons.donut_large )
              ),
              ListTile(
                title: Text( "Sample Tile 3" ),
                trailing: Icon( Icons.donut_small )
              ),
              Divider(),
              ListTile(
                title: Text( 'Log Out' ),
                trailing: Icon( Icons.exit_to_app ),
                onTap: () {
                  Navigator.pop( context );
                }
              )
            ],
          )
        ),
        body: Center(
          child: FutureBuilder<Shippers>(
            future: fetchPost(),
            builder: (context, snapshot) {
              if (snapshot.hasData) {

                String output = '';

                for ( int i = 0; i < snapshot.data.posts.length; i++ )
                {
                  output += ( 
                            ' ShipperID:   ${ snapshot.data.posts[ i ].shipperID } \n' +
                            ' ShipperName: ${ snapshot.data.posts[ i ].companyName } \n' 
                           // ' Shipper Phone: ${ snapshot.data.posts[ i ].phone } \n\n\n' 
                            );
                }
                return Text(
                  output
                );
              } 
              else if (snapshot.hasError)
              {
                return Text("${snapshot.error}");
              }
              return CircularProgressIndicator();
            },
          ),
        ),
        bottomNavigationBar: BottomNavigationBar( 
          items: [ 
            BottomNavigationBarItem(
              icon: Icon( Icons.home ),
              title: Text( 'Home' )
            ),
            BottomNavigationBarItem(
              icon: Icon( Icons.search ),
              title: Text( 'Search For Item' )
            )
          ] 
        )
      ),
    );
  }
}