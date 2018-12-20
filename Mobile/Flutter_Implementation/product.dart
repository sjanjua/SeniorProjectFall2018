import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;
import 'globals.dart';

//**************************************************************************************** 
//Product.dart 
//adds the functionality for products to the app that is accessible from then home page  
//**************************************************************************************** 

//Fetch Post for the endpoint from the rest api 
Future< ProductsList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/products'); //endpoint 
  
  if (response.statusCode == 200)
  {
    return ProductsList.fromJson( json.decode( response.body ) ); //decoding the JSON endpoint 
  } 
  else 
  {
    throw Exception( 'Failed to load post' ); //error message if the endpoint cannot be retrieved 
  }
}

//Products List class 
//Parsing the JSON endpoint to be used to make the products list 
class ProductsList 
{
  final List< ProductsPost > posts;

  ProductsList( { this.posts } );

  factory ProductsList.fromJson( List< dynamic > parsedJson )
  {
    List< ProductsPost > products = new List< ProductsPost >();
    products = parsedJson.map( ( i ) => ProductsPost.fromJson( i ) ).toList();

    return ProductsList( posts: products );
  }
}

//Products Post class that assigns each product post with the corresponding 
//JSON attributes 
class ProductsPost
{
  final int    productID;
  final String productName;
  final String quantityPerUnit;
  final double unitPrice;
  final int    unitsInStock;

  ProductsPost( 
    { 
      this.productID, 
      this.productName, 
      this.quantityPerUnit, 
      this.unitPrice, 
      this.unitsInStock 
    });

  factory ProductsPost.fromJson (Map< String, dynamic > json )
  {
    return ProductsPost(
      productID:       json[ 'ProductID'],
      productName:     json[ 'ProductName'],
      quantityPerUnit: json[ 'QuantityPerUnit' ],
      unitPrice:       json[ 'UnitPrice' ],
      unitsInStock:    json[ 'UnitsInStock' ]
    );
  }
}

//Product class 
//defines the attributes that each product should have 
class Product
{
  int    productID;
  String productName;
  String quantityPerUnit;
  double unitPrice;
  int    unitsInStock;
  Color  colorIndicator;

  Product(
    {
      this.productID,
      this.productName,
      this.quantityPerUnit,
      this.unitPrice,
      this.unitsInStock,
      this.colorIndicator
    }
  );
}

//Instantiate a Products widget 
class ProductsWidg extends StatefulWidget 
{
  ProductsWidg({Key key}) : super( key: key);
  @override
  _ProductsWidgState createState() => new _ProductsWidgState();
}

//Set up the Widget and the text in the widget to display the products list 
class _ProductsWidgState extends State< ProductsWidg > 
{

  final key = new GlobalKey<ScaffoldState>();
  List< Product > _list;

  //Building the widget 
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: Globals.backgroundColor,
      key: key,
      body: FutureBuilder< ProductsList >(
        future: fetchPost(),
        builder: ( context, snapshot ) {
          if ( snapshot.hasData )
          {
            _list = List();
            Product _product;

            for ( int i = 0; i < snapshot.data.posts.length; i++ )
            {
              Color color = Globals.barColor;

              if ( snapshot.data.posts[ i ].unitsInStock <= 20 ) //color coded red if there are less than 20 units 
              {
                color = Colors.red;
              }
              else if ( snapshot.data.posts[ i ].unitsInStock <= 50 ) //yellow otherwise 
              {
                color = Colors.yellow;
              }

              _product = Product(
                productID:       snapshot.data.posts[ i ].productID,
                productName:     snapshot.data.posts[ i ].productName.toString(),
                quantityPerUnit: snapshot.data.posts[ i ].quantityPerUnit,
                unitPrice:       snapshot.data.posts[ i ].unitPrice,
                unitsInStock:    snapshot.data.posts[ i ].unitsInStock,
                colorIndicator:  color
              );

              _list.add( _product );
            }

            return ListView.builder(
              itemCount: _list.length,
              itemBuilder: ( context, index ) {
                return Card(
                  elevation: 8.0,
                  margin: new EdgeInsets.symmetric( horizontal: 10.0, vertical: 10.0 ),
                  child: Container(
                    decoration: BoxDecoration( 
                      color: Globals.barColor, 
                      border: Border( 
                        right: BorderSide( 
                          color: _list.elementAt( index ).colorIndicator,
                          width: 6 
                        ) 
                      ) 
                    ),
                    child: ListTile(
                      contentPadding: EdgeInsets.symmetric( horizontal: 10.0, vertical: 6.0 ),
                      title: Row(
                        children: <Widget>[
                          Text( 
                            'Product ID:        ' + '\n' +
                            'Product Name:      ' + '\n' +
                            'Quantity / Unit:   ' + '\n' +
                            'Unit Price:        ' + '\n' +
                            'Units In Stock:    ',
                            style: Globals.textStyle
                          ),
                          Text( 
                            _list.elementAt( index ).productID.toString()       + '\n' +
                            _list.elementAt( index ).productName.toString()     + '\n' +
                            _list.elementAt( index ).quantityPerUnit.toString() + '\n' +
                            _list.elementAt( index ).unitPrice.toString()       + '\n' +
                            _list.elementAt( index ).unitsInStock.toString(),
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
  }
}

// ListTile(
//                   title: Text(
//                     'Product ID:        ' + _list.elementAt( index ).productID.toString()       + '\n' +
//                     'Product Name:      ' + _list.elementAt( index ).productName.toString()     + '\n' +
//                     'Quantity Per Unit: ' + _list.elementAt( index ).quantityPerUnit.toString() + '\n' +
//                     'Unit Price:        ' + _list.elementAt( index ).unitPrice.toString()       + '\n' +
//                     'Units In Stock:    ' + _list.elementAt( index ).unitsInStock.toString()    + '\n'
//                   )
//                 );
