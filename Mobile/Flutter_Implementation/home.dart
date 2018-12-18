import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'searchForItem.dart';
import 'package:http/http.dart' as http;
import 'shippers.dart';
import 'suppliers.dart';
import 'customers.dart';
import 'product.dart';
import 'orders.dart';
import 'naturallanguagesearch.dart';
import 'globals.dart';

Future<Shippers> fetchPost() async
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/');

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

    return Shippers( posts: postList );
  }
}

class Post
{
  final int    shipperID;
  final String companyName;
  final String phone;

  Post({this.shipperID, this.companyName, this.phone});

  factory Post.fromJson( Map< String, dynamic > json )
  {
    return Post(
        shipperID: json[ 'ShipperID' ],
        companyName: json[ 'ShipperName' ],
        phone: json[ 'Phone' ]
    );
  }
}

class HomeWidg extends StatefulWidget
{
  String _username;

  HomeWidg( { Key key, String username } ) : super( key: key )
  {
    _username = username;
  }

  @override
  _HomeWidgState createState() => _HomeWidgState( _username );
}

class _HomeWidgState extends State< HomeWidg >
{
  String _username;

  _HomeWidgState( String username )
  {
    _username = username;
  }


  ProductsWidg products;

  @override
  void initState()
  {
    super.initState();
    products = ProductsWidg();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Inventory Management System',
      theme: ThemeData(
        brightness: Brightness.dark,
        primaryColor: Globals.barColor
      ),
      home: Scaffold(
          backgroundColor: Globals.backgroundColor,
          appBar: AppBar(
            centerTitle: true,
            title: Text( 'Dashboard - Current Inventory', style: Globals.textStyle ), 
            actions: <Widget>[
              IconButton( 
                icon: Icon( Icons.refresh ),
                onPressed: () { 
                  setState( () {
                    products = ProductsWidg();
                  });
                },
              )
            ],
          ),
          drawer: sideDrawer( context, _username ),
          body: products,
          bottomNavigationBar: BottomAppBar(
              color: Globals.barColor,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                mainAxisSize: MainAxisSize.max,
                children: <Widget>[
                  IconButton(
                    icon: Icon( Icons.search ),
                    onPressed: () {}
                  )
                ],
              )
          )
      ),
    );
  }
}

Drawer sideDrawer( BuildContext context, String _username )
{
  return Drawer(
    child: ListView(
      children: <Widget>[
        UserAccountsDrawerHeader(
            accountName: Text( 'Current User:   ' + _username, style: Globals.textStyle ),
            accountEmail: Text( "" )
        ),
        ListTile(
            title: Text( "Shippers", style: Globals.textStyle ),
            trailing: Icon( Icons.android ),
            onTap: () {
              Navigator.push( context, MaterialPageRoute( builder: ( context ) => ShippersWidg() ) );
            },
        ),
        ListTile(
            title: Text( "Suppliers", style: Globals.textStyle ),
            trailing: Icon( Icons.donut_large ),
            onTap: (){
              Navigator.push( context, MaterialPageRoute( builder: ( context ) => SuppliersWidg() ));
            },
        ),
        ListTile(
            title: Text( "Customers", style: Globals.textStyle ),
            trailing: Icon( Icons.donut_small ),
            onTap: (){
              Navigator.push( context, MaterialPageRoute( builder: ( context ) => CustomersWidg() ));
            }
        ),
        ListTile(
          title: Text( "Orders", style: Globals.textStyle ),
          trailing: Icon( Icons.shopping_basket ),
          onTap: () {
            Navigator.push( context, MaterialPageRoute( builder: ( context ) => OrdersWidg() ) );
          }
        ),
        ListTile(
          title: Text( "Natural Language Search", style: Globals.textStyle ),
          trailing: Icon( Icons.cloud ),
          onTap: () {
            Navigator.push( context, MaterialPageRoute( builder: ( context ) => NLSWidg() ) );
          }
        ),
        Divider(),
        ListTile(
            title: Text( 'Log Out', style: Globals.textStyle ),
            trailing: Icon( Icons.exit_to_app ),
            onTap: () {
              Navigator.pop( context );
            }
        )
      ],
    )
  );
}