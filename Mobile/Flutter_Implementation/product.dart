import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;

Future< ProductsList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/products');
  
  if (response.statusCode == 200)
  {
    return ProductsList.fromJson( json.decode( response.body ) );
  } 
  else 
  {
    throw Exception( 'Failed to load post' );
  }
}

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

class Product
{
  int    productID;
  String productName;
  String quantityPerUnit;
  double unitPrice;
  int    unitsInStock;

  Product(
    {
      this.productID,
      this.productName,
      this.quantityPerUnit,
      this.unitPrice,
      this.unitsInStock
    }
  );
}

class ProductsWidg extends StatefulWidget 
{
  ProductsWidg({Key key}) : super(key: key);
  @override
  _ProductsWidgState createState() => new _ProductsWidgState();
}

class _ProductsWidgState extends State< ProductsWidg > 
{

  final key = new GlobalKey<ScaffoldState>();
  List< Product > _list;

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
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
              // String productName = snapshot.data.posts[ i ].productName.toString();
              _product = Product(
                productID:       snapshot.data.posts[ i ].productID,
                productName:     snapshot.data.posts[ i ].productName.toString(),
                quantityPerUnit: snapshot.data.posts[ i ].quantityPerUnit,
                unitPrice:       snapshot.data.posts[ i ].unitPrice,
                unitsInStock:    snapshot.data.posts[ i ].unitsInStock
              );

              _list.add( _product );
            }

            return ListView.builder(
              itemCount: _list.length,
              itemBuilder: ( context, index ) {
                return ListTile(
                  title: Text(
                    'Product ID:        ' + _list.elementAt( index ).productID.toString()       + '\n' +
                    'Product Name:      ' + _list.elementAt( index ).productName.toString()     + '\n' +
                    'Quantity Per Unit: ' + _list.elementAt( index ).quantityPerUnit.toString() + '\n' +
                    'Unit Price:        ' + _list.elementAt( index ).unitPrice.toString()       + '\n' +
                    'Units In Stock:    ' + _list.elementAt( index ).unitsInStock.toString()    + '\n'
                  )
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
}
