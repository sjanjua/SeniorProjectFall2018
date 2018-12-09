import 'package:flutter/material.dart';
import 'dart:io';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'orders.dart';

class NLSWidg extends StatefulWidget
{
  NLSWidg( {Key key } ) : super( key: key );

  @override 
  _NLSWidgState createState() => _NLSWidgState();

}

class _NLSWidgState extends State< NLSWidg >
{

  String queryField = 'n/a';
  TextEditingController controller = TextEditingController();
  String queryResponse = 'n/a';

  @override 
  Widget build( BuildContext context )
  {
    return MaterialApp(
      theme: ThemeData( 
        primarySwatch: Colors.blueGrey
      ),
      home: Scaffold(
        body: Center(
          child: Padding(
            padding: EdgeInsets.fromLTRB( 30.0, 100.0, 30.0, 50.0 ),
            child: Column(
              children: <Widget>[
                TextField( controller: controller ),
                Text( queryResponse ),
                MaterialButton( 
                  onPressed: () {
                    queryField = controller.text;

                    QueryPost post = QueryPost( query: queryField );

                    createQueryPost( post ).then(
                      ( response )
                      {
                        print( response.body );
                        OrdersList products = OrdersList.fromJson( json.decode( response.body ) );

                        for ( int i = 0; i < products.posts.length; i++ )
                        {
                          print( products.posts[ i ].freightNumber.toString()   + '\n' + 
                                 products.posts[ i ].orderDate.toString() + '\n' + '\n' );
                        }
                  
                        queryResponse = response.body;
                      }
                    );
                  },
                  child: Text( 'Submit' ),
                  color: Colors.blueGrey,
                  elevation: 15.0
                )
              ]
            )
          )
        )
      ) 
    );
  }

  Future< http.Response > createQueryPost( QueryPost post ) async
  {
    final response = await http.post(
      Uri.parse( 'http://inv.azurewebsites.net/api/nls' ),
      headers: { HttpHeaders.contentTypeHeader: 'application/json' },
      body: postToJson( post )
    );

    return response;
  }

  String postToJson( QueryPost data )
  {
    final dyn = data.toJson();
    return json.encode( dyn );
  }

  QueryPost postFromJson( String str )
  {
    final jsonData = json.decode( str );
    return QueryPost.fromJson( jsonData );
  }

}

class QueryPost
{
  String query;

  QueryPost( { this.query } );

  factory QueryPost.fromJson( Map< String, dynamic > json ) => QueryPost(
    query: json[ 'Query' ]
  );

  Map< String, dynamic > toJson() =>
  {
    "Query": query
  };
}