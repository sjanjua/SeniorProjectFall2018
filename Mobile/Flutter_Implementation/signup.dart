import 'package:flutter/material.dart';
import 'login.dart';

class SignUpScreen extends StatelessWidget
{
  @override
  Widget build( BuildContext context )
  {
    return MaterialApp(
      title: 'Sign Up',
      theme: ThemeData(
        primarySwatch: Colors.blueGrey
      ),
      home: Builder(
        builder: ( context ) => Scaffold(
          appBar: AppBar(
            title: Text( 'Sign Up' ),
            actions: <Widget>[
              IconButton(
                icon: Icon( Icons.exit_to_app ),
                onPressed: ( ) {
                  Navigator.pop( context );
                  Navigator.push( context, MaterialPageRoute( builder: ( context ) => LogInScreen() ) );
                }
              )
            ],
          ),
          body: Container(
            padding: EdgeInsets.fromLTRB(30.0, 50.0, 30.0, 30.0 ),
            alignment: Alignment.center,
            child: Column(
              children: <Widget>[
                Text(
                  'Create Account',
                  style: TextStyle(
                    fontSize: 35.0,
                    fontWeight: FontWeight.bold
                  ),
                ),
                Container(
                  padding: EdgeInsets.all( 20.0 ),
                  child: Column(
                    children: <Widget>[
                      Text(
                        'Personal Information',
                        style: TextStyle(
                        fontSize: 20.0,
                        fontWeight: FontWeight.bold
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'First Name'
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'Last Name'
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'Phone'
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'Street'
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'City'
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'Zip Code'
                        )
                      ),
                      TextField(
                        decoration: InputDecoration(
                          hintText: 'Email'
                        )
                      ),
                      Container(
                        padding: EdgeInsets.all( 30.0 ),
                        alignment: Alignment.center,
                        child: Column(
                          children: <Widget>[
                            Text(
                              'Login Credentials',
                              style: TextStyle(
                                fontSize: 20.0,
                                fontWeight: FontWeight.bold
                              )
                            ),
                            TextField(
                              decoration: InputDecoration(
                                hintText: 'Username'
                              )
                            ),
                            TextField(
                              decoration: InputDecoration(
                                hintText: 'Password'
                              )
                            ),
                            TextField(
                              decoration: InputDecoration(
                                hintText: 'Confirm Password'
                              )
                            ),
                            RaisedButton(
                              onPressed: () {
                                Navigator.pop( context );
                                Navigator.push( context, MaterialPageRoute( builder: ( context ) => LogInScreen() ) );
                              },
                              color: Colors.cyan,
                              child: Text( 'Sign Up!' )
                            )
                          ],
                        ),
                      )
                    ],
                  )
                )
              ],
            )
          )
        )
      )
    );
  }
}