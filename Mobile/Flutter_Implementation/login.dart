import 'package:flutter/material.dart';
import 'home.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'dart:io';
import 'globals.dart';

class logInScreen extends StatelessWidget
{
  final GlobalKey< FormState > _formKey = GlobalKey< FormState >();
  bool _autoValidate = false;

  LoginPost post;
  String _username;
  String _password;

  @override
  Widget build( BuildContext context )
  {
    return MaterialApp(
        title: 'Login Screen',
        theme: ThemeData(
          brightness: Brightness.dark,
          primaryColor: Globals.barColor,
        ),
        home: Builder(
            builder: ( context ) => Scaffold(
                backgroundColor: Globals.backgroundColor,
                appBar: AppBar(
                  centerTitle: true,
                  title: Text( 'Inventory Management System', style: Globals.textStyle )
                ),
                body: Center(
                    child: Container(
                        padding: EdgeInsets.fromLTRB( 35.0, 150.0, 35.0, 35.0 ),
                        child: Column(
                            children: <Widget>[
                              Form(
                                  key: _formKey,
                                  autovalidate: _autoValidate,
                                  child: formUI()
                              ),
                              Container(
                                padding: EdgeInsets.fromLTRB(0.0, 60.0, 0.0 , 0.0),
                                child: Column(
                                  children: <Widget>[
                                    RaisedButton(
                                      onPressed: () {

                                        if ( _formKey.currentState.validate() ) {

                                          post = LoginPost( username: _username, password: _password );

                                          createLoginPost( post ).then(

                                            ( response ) {

                                                print( response.statusCode );
                                                print( response.body );

                                                if ( response.statusCode == 200 )
                                                {
                                                  if ( response.body == "{\"Message\":\"Success\"}" )
                                                  {
                                                    Navigator.push( context, MaterialPageRoute( builder: ( context ) => HomeWidg( username: _username, ) ) );
                                                  }

                                                else
                                                {
                                                  showDialog(
                                                    context: context,
                                                    builder: ( context ) => SimpleDialog( 
                                                      children: <Widget>[
                                                        Text( 
                                                          'Incorrect username and/or password',
                                                          style: TextStyle( 
                                                            fontWeight: FontWeight.bold 
                                                          ),
                                                          textAlign: TextAlign.center    
                                                        )
                                                      ],
                                                    )
                                                  );
                                                }

                                                }

                                                else
                                                {
                                                  showDialog(
                                                    context: context,
                                                    builder: ( context ) => SimpleDialog( 
                                                      children: <Widget>[
                                                        Text( 
                                                          'Incorrect username and/or password',
                                                          style: TextStyle( 
                                                            fontWeight: FontWeight.bold 
                                                          ),
                                                          textAlign: TextAlign.center    
                                                        )
                                                      ],
                                                    )
                                                  );
                                                }
                                              }
                                          );
                                        }
                                      },
                                      color: Colors.cyan[ 700 ],
                                      child: Text( 'Login', style: Globals.textStyle ),
                                      elevation: 15.0,
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

  Widget formUI() {

    Column column = Column(
        children: < Widget >[
          TextFormField(
              decoration: InputDecoration( labelText: 'email', icon: Icon( Icons.email ) ),
              keyboardType: TextInputType.emailAddress,
              validator: ( value ) {
                if ( value.isEmpty )
                {
                  return 'Please enter email';
                }
                else
                {
                  _username = value;
                }
              }
          ),
          TextFormField(
            decoration: InputDecoration( labelText: 'password', icon: Icon( Icons.lock ) ),
            keyboardType: TextInputType.text,
            validator: ( value ) {
              if ( value.isEmpty )
              {
                return 'Please enter password';
              }
              else
              {
                _password = value;
              }
            },
            obscureText: true,
          )
        ]
    );

    return column;
  }

  LoginPost postFromJson( String str )
  {
    final jsonData = json.decode( str );
    return LoginPost.fromJson( jsonData );
  }

  String postToJson( LoginPost data )
  {
    final dyn = data.toJson();
    return json.encode( dyn );
  }

  Future< http.Response > createLoginPost( LoginPost post ) async
  {
    final response = await http.post(
        Uri.parse( 'http://inv.azurewebsites.net/api/account' ),
        headers: { HttpHeaders.contentTypeHeader: 'application/json' },
        body: postToJson( post )
    );

    return response;
  }

}

class LoginPost
{
  String username;
  String password;

  LoginPost( {this.username, this.password} );

  factory LoginPost.fromJson( Map< String, dynamic > json ) => new LoginPost(
      username: json[ "UserName" ],
      password: json[ "Password" ]
  );

  Map< String, dynamic > toJson() =>
      {
        "UserName": username,
        "Password": password
      };
}
