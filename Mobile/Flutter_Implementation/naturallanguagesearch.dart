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
      // theme: ThemeData( 
      //   brightness: Brightness.dark
      // ),
      home: Scaffold(
        body: Center(
          child: Padding(
            padding: EdgeInsets.fromLTRB( 30.0, 100.0, 30.0, 50.0 ),
            child: Column(
              children: <Widget>[
                TextField( controller: controller ),
                SingleChildScrollView(
                  child: Text( queryResponse )
                ),
                MaterialButton( 
                  onPressed: () {
                    queryField = controller.text;

                    QueryPost post = QueryPost( query: queryField );

                    createQueryPost( post ).then(
                      ( response )
                      {
                        OrdersList products = OrdersList.fromJson( json.decode( response.body ) );
                        String buffer = '';
                        String str = '';

                        for ( int i = 0; i < products.posts.length; i++ )
                        {
                          buffer = '------------------------' + '\n' + 
                                    products.posts[ i ].orderID.toString()        + '\n' + 
                                    products.posts[ i ].orderDate.toString()      + '\n' +
                                    products.posts[ i ].requiredDate.toString()   + '\n' +
                                    products.posts[ i ].shippedDate.toString()    + '\n' +
                                    products.posts[ i ].shippedAddress.toString() + '\n' +
                                    products.posts[ i ].shippedCity.toString()    + '\n' +
                                    products.posts[ i ].shippedRegion.toString()  + '\n' +
                                    products.posts[ i ].shippedName.toString()    + '\n' +
                                    products.posts[ i ].freightNumber.toString()  + '\n' +
                                    products.posts[ i ].userName.toString()       + '\n' + 
                                    '------------------------' + '\n';
                          str += buffer;
                          print( buffer );
                        }
                  
                        queryResponse = str;
                        
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

// TextField( controller: controller ),
//                 Text( queryResponse ),
//                 MaterialButton( 
//                   onPressed: () {
//                     queryField = controller.text;

//                     QueryPost post = QueryPost( query: queryField );

//                     createQueryPost( post ).then(
//                       ( response )
//                       {
//                         OrdersList products = OrdersList.fromJson( json.decode( response.body ) );
//                         String buffer = '';
//                         String str = '';

//                         for ( int i = 0; i < products.posts.length; i++ )
//                         {
//                           buffer = '------------------------' + '\n' + 
//                                     products.posts[ i ].orderID.toString()        + '\n' + 
//                                     products.posts[ i ].orderDate.toString()      + '\n' +
//                                     products.posts[ i ].requiredDate.toString()   + '\n' +
//                                     products.posts[ i ].shippedDate.toString()    + '\n' +
//                                     products.posts[ i ].shippedAddress.toString() + '\n' +
//                                     products.posts[ i ].shippedCity.toString()    + '\n' +
//                                     products.posts[ i ].shippedRegion.toString()  + '\n' +
//                                     products.posts[ i ].shippedName.toString()    + '\n' +
//                                     products.posts[ i ].freightNumber.toString()  + '\n' +
//                                     products.posts[ i ].userName.toString()       + '\n' + 
//                                     '------------------------' + '\n';
//                           str += buffer;
//                           print( buffer );
//                         }
                  
//                         queryResponse = str;
//                       }
//                     );
//                   },
//                   child: Text( 'Submit' ),
//                   color: Colors.blueGrey,
//                   elevation: 15.0
//                 )