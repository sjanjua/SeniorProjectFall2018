import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class OrderWidg extends StatelessWidget
{
  final GlobalKey< FormState > _formKey = GlobalKey< FormState >();
  bool _autoValidate = false;

  @override
  Widget build( BuildContext context )
  {
    return MaterialApp(
        title: 'Order Page',
        theme: ThemeData(
            primarySwatch: Colors.blueGrey
        ),
        home: Builder(
            builder: ( context ) => Scaffold(
                appBar: AppBar(
                    title: Text( 'Create an Order' )
                ),
                body: Center(
                    child: Container(
                        padding: EdgeInsets.fromLTRB( 35.0, 200.0, 35.0, 35.0 ),
                        child: Column(
                            children: <Widget>[

                              Container(
                                padding: EdgeInsets.fromLTRB(0.0, 50.0, 0.0 , 0.0),
                                child: Column(
                                  children: <Widget>[
                                    RaisedButton(

                                      color: Colors.cyan,
                                      child: Text( 'Create Order' ),
                                    ),
                                  ],
                                ),
                              )
                            ]
                        )
                    )
                )
            )
        )
    );
  }



// TextField(
//                   decoration: InputDecoration(
//                     icon: Icon( Icons.email ),
//                     hintText: 'email',
//                   ),
//                 ),
//                 TextField(
//                   decoration: InputDecoration(
//                     icon: Icon( Icons.lock ),
//                     hintText: 'password'
//                   ),
//                 ),
}