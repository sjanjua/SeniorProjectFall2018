import 'package:flutter/material.dart';
import 'dart:io';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'orders.dart';
import 'globals.dart';

//******************************************************************************************* 
//Natural language search capabilities being added to the order aspect of the app 
//Promotes for a more enhanced user experience and easier searches 
//******************************************************************************************* 


//Instantiating a natural language search widget 
class NLSWidg extends StatefulWidget
{
  NLSWidg( {Key key } ) : super( key: key );

  @override 
  _NLSWidgState createState() => _NLSWidgState();
}



//Building and formatting the widget that user will have access to in the app 
class _NLSWidgState extends State< NLSWidg >
{

  String queryField = 'n/a';
  TextEditingController controller = TextEditingController();
  String queryResponse = 'n/a';
  OrdersList products;

  @override
  void initState()
  {
    super.initState();

    products = OrdersList(
      posts: < OrdersPost >[
        OrdersPost()
      ]
    );
  }

  //Construction of the widget 
  @override 
  Widget build( BuildContext context )
  {
    return MaterialApp(
      theme: ThemeData( 
        brightness: Brightness.dark,
        primaryColor: Globals.barColor
      ),
      home: Scaffold(
        backgroundColor: Globals.backgroundColor,
        body: Center(
          child: Padding(
            padding: EdgeInsets.fromLTRB( 30.0, 60.0, 30.0, 50.0 ),
            child: Column(
              children: <Widget>[
                TextField( controller: controller, decoration: InputDecoration( hintText: 'Enter query to search for orders' ) ), //prompting user for query 
                MaterialButton( 
                  onPressed: () {
                    queryField = controller.text;

                    QueryPost post = QueryPost( query: queryField );

                    createQueryPost( post ).then(
                      ( response )
                      {
                        
                        // String buffer = '';
                        // String str = '';

                        // for ( int i = 0; i < products.posts.length; i++ )
                        // {
                        //   buffer = '------------------------' + '\n' + 
                        //             products.posts[ i ].orderID.toString()        + '\n' + 
                        //             products.posts[ i ].orderDate.toString()      + '\n' +
                        //             products.posts[ i ].requiredDate.toString()   + '\n' +
                        //             products.posts[ i ].shippedDate.toString()    + '\n' +
                        //             products.posts[ i ].shippedAddress.toString() + '\n' +
                        //             products.posts[ i ].shippedCity.toString()    + '\n' +
                        //             products.posts[ i ].shippedRegion.toString()  + '\n' +
                        //             products.posts[ i ].shippedName.toString()    + '\n' +
                        //             products.posts[ i ].freightNumber.toString()  + '\n' +
                        //             products.posts[ i ].userName.toString()       + '\n' + 
                        //             '------------------------' + '\n';
                        //   str += buffer;
                        //   //print( buffer );
                        // }
                  
                        // queryResponse = str;

                        setState( () {
                          products = OrdersList.fromJson( json.decode( response.body ) ); 
                        });
                        
                      }
                    );
                  },
                  child: Text( 'Submit', style: Globals.textStyle ),
                  color: Colors.cyan[ 700 ],
                  elevation: 15.0
                ),
                Expanded( 
                  child: ListView.builder(
                  itemCount: products.posts.length,
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
                            products.posts.elementAt( index ).orderID.toString()        + '\n' +
                            products.posts.elementAt( index ).orderDate.toString()      + '\n' +
                            products.posts.elementAt( index ).requiredDate.toString()   + '\n' +
                            products.posts.elementAt( index ).shippedDate.toString()    + '\n' +
                            products.posts.elementAt( index ).shippedAddress.toString() + '\n' +
                            products.posts.elementAt( index ).shippedCity.toString()    + '\n' +
                            products.posts.elementAt( index ).shippedRegion.toString()  + '\n' +
                            products.posts.elementAt( index ).shippedName.toString()    + '\n' +
                            products.posts.elementAt( index ).freightNumber.toString()  + '\n' +
                            products.posts.elementAt( index ).userName.toString(),
                            style: Globals.textStyle
                          )
                        ],
                      )
                    )
                  )
                );},  
                // child: Text( queryResponse )
              ),
                )
              ]
            )
          )
        )
      ) 
    );
  }//END of Natural language search widget 

  
  //gets the endpoint for natural language search usage using a Query Post 
  //returns the response 
  Future< http.Response > createQueryPost( QueryPost post ) async
  {
    final response = await http.post(
      Uri.parse( 'http://inv.azurewebsites.net/api/nls' ),              //rest api endpoint 
      headers: { HttpHeaders.contentTypeHeader: 'application/json' },
      body: postToJson( post )
    );

    return response;
  }//END of create query post Response is returned 

  
  //Post the queryPost data to JSON 
  String postToJson( QueryPost data )
  {
    final dyn = data.toJson();
    return json.encode( dyn );
  }//END postToJson 

  
  //decode queryPost data from Json 
  QueryPost postFromJson( String str )
  {
    final jsonData = json.decode( str );
    return QueryPost.fromJson( jsonData );
  }//END postFromJson 

}//END of NLSWidgetState 

//Class for QueryPost 
//Assigns the Json value for a query to the local varaible for that attribute 
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
}//END of QueryPost class 
//END OF NATURAL LANGUAGE SEARCH CODE
