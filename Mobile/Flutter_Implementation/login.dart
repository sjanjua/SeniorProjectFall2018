import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';

class LogInScreen extends StatelessWidget
{
  final GlobalKey< FormState > _formKey = GlobalKey< FormState >();
  bool _autoValidate = false;

  String _email;
  String _password;

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
                  Form(
                    key: _formKey,
                    autovalidate: _autoValidate,
                    child: FormUI()
                  ),
                  Container(
                    padding: EdgeInsets.fromLTRB(0.0, 50.0, 0.0 , 0.0),
                    child: Column(
                      children: <Widget>[
                        RaisedButton( 
                          onPressed: () {
                          
                            if ( _formKey.currentState.validate() ) {

                              if ( _email == 'admin' && _password == 'admin' )
                              {
                                Navigator.push( context, MaterialPageRoute( builder: ( context ) => Home() ) );
                              }

                              // Navigator.push( context, MaterialPageRoute( builder: ( context ) => Home() ) );

                            }
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

  Widget FormUI() {

    return Column(
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
              _email = value;
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
  }
}
