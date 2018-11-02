import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';

class LogInScreen extends StatelessWidget
{
  @override
  Widget build( BuildContext context )
  {
    return MaterialApp( 
      title: 'Login Screen',
      theme: ThemeData(
        primarySwatch: Colors.blueGrey
      ),
      home: Builder(
        builder: ( context ) => Scaffold(
          appBar: AppBar(
            title: Text( 'Inventory Management System' )
          ),
          body: Center(
            child: Container(
              padding: EdgeInsets.fromLTRB( 35.0, 200.0, 35.0, 35.0 ),
              child: Column(
                children: <Widget>[
                  TextField(
                    decoration: InputDecoration(
                      icon: Icon( Icons.email ),
                      hintText: 'email', 
                    ),
                  ),
                  TextField(
                    decoration: InputDecoration(
                      icon: Icon( Icons.lock ),
                      hintText: 'password'
                    ),
                  ), 
                  Container(
                    padding: EdgeInsets.fromLTRB(0.0, 50.0, 0.0 , 0.0),
                    child: Column(
                      children: <Widget>[
                        RaisedButton( 
                          onPressed: () {
                            Navigator.push( context, MaterialPageRoute( builder: ( context ) => Home() ) );
                          },
                          color: Colors.cyan,
                          child: Text( 'Login' ),
                        ),
                        RaisedButton(
                          onPressed: () {
                            Navigator.push( context, MaterialPageRoute( builder: ( context ) => SignUpScreen() ) );
                          },
                          color: Colors.cyan,
                          child: Text( 'Sign Up' )
                        )
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
}
