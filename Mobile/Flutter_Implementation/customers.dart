import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;

Future< CustomersList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/customers');
  
  if (response.statusCode == 200)
  {
    return CustomersList.fromJson( json.decode( response.body ) );
  } 
  else 
  {
    throw Exception( 'Failed to load post' );
  }
}

class CustomersList 
{
  final List< CustomersPost > posts;

  CustomersList( { this.posts } );

  factory CustomersList.fromJson( List< dynamic > parsedJson )
  {
    List< CustomersPost > customers = new List< CustomersPost >();
    customers = parsedJson.map( ( i ) => CustomersPost.fromJson( i ) ).toList();

    return CustomersList( posts: customers );
  }
}

class CustomersPost
{
  final String customerID;
  final String companyName;

  CustomersPost( { this.customerID, this.companyName } );

  factory CustomersPost.fromJson (Map< String, dynamic > json )
  {
    return CustomersPost(
      customerID:  json['CustomerID'],
      companyName: json['CompanyName'],
    );
  }
}

class CustomersWidg extends StatefulWidget 
{
  CustomersWidg({Key key}) : super(key: key);
  @override
  _CustomersWidgState createState() => new _CustomersWidgState();
}

class _CustomersWidgState extends State< CustomersWidg > 
{
  Widget appBarTitle = new Text(
    "Search For Customers",
    style: new TextStyle(color: Colors.white),
  );
  Icon actionIcon = new Icon(
    Icons.search,
    color: Colors.white,
  );

  final key = new GlobalKey<ScaffoldState>();
  final TextEditingController _searchQuery = new TextEditingController();
  List<String> _list;
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

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      key: key,
      appBar: buildBar(context),
      body: FutureBuilder< CustomersList >(
        future: fetchPost(),
        builder: ( context, snapshot ) {
          if ( snapshot.hasData )
          {
            _list = List();

            for ( int i = 0; i < snapshot.data.posts.length; i++ )
            {
              String customerName = snapshot.data.posts[ i ].companyName.toString();
              _list.add( customerName );
            }

            return ListView.builder(
              itemCount: _list.length,
              itemBuilder: ( context, index ) {
                return ListTile(
                  title: Text( _list[ index ] )
                );
              }
            );
          }

          else
          {
            return Text( 'Error loading data...' );
          }
        }
      )
    );
  }

  List<ChildItem> _buildList() {
    return _list.map((contact) => new ChildItem(contact)).toList();
  }

  List<ChildItem> _buildSearchList() {
    if (_searchText.isEmpty) {
      return _list.map((contact) => new ChildItem(contact)).toList();
    } else {
      List<String> _searchList = List();
      for (int i = 0; i < _list.length; i++) {
        String name = _list.elementAt(i);
        if (name.toLowerCase().contains(_searchText.toLowerCase())) {
          _searchList.add(name);
        }
      }
      return _searchList.map((contact) => new ChildItem(contact)).toList();
    }
  }

  Widget buildBar(BuildContext context) {
    return new AppBar(centerTitle: true, title: appBarTitle, actions: <Widget>[
      new IconButton(
        icon: actionIcon,
        onPressed: () {
          setState(() {
            if (this.actionIcon.icon == Icons.search) {
              this.actionIcon = new Icon(
                Icons.close,
                color: Colors.white,
              );
              this.appBarTitle = new TextField(
                controller: _searchQuery,
                style: new TextStyle(
                  color: Colors.white,
                ),
                decoration: new InputDecoration(
                    prefixIcon: new Icon(Icons.search, color: Colors.white),
                    hintText: "Search...",
                    hintStyle: new TextStyle(color: Colors.white)),
              );
              _handleSearchStart();
            } else {
              _handleSearchEnd();
            }
          });
        },
      ),
    ]);
  }

  void _handleSearchStart() {
    setState(() {
      _IsSearching = true;
    });
  }

  void _handleSearchEnd() {
    setState(() {
      this.actionIcon = new Icon(
        Icons.search,
        color: Colors.white,
      );
      this.appBarTitle = new Text(
        "Search Sample",
        style: new TextStyle(color: Colors.white),
      );
      _IsSearching = false;
      _searchQuery.clear();
    });
  }
}

class ChildItem extends StatelessWidget {
  final String name;
  ChildItem(this.name);
  @override
  Widget build(BuildContext context) {
    return new ListTile(title: new Text(this.name));
  }
}