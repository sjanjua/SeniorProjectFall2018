import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:http/http.dart' as http;

class OrderWidg extends StatefulWidget {
  OrderWidg({Key key}) : super(key: key);

  @override
  _OrderWidgState createState() => _OrderWidgState();
}

class _OrderWidgState extends State<OrderWidg> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  bool _autoValidate = false;

  List<String> _customers = [
    "Alfreds Futterkiste",
    "Ana Trujillo Emparedados y helados",
    "Antonio Moreno Taquera",
    "Around the Horn",
    "Berglunds snabbkp",
    "Blauer See Delikatessen",
    "Blondesddsl pre et fils",
    "Blido Comidas preparadas",
    "Bon app'",
    "Bottom-Dollar Markets",
    "B's Beverages",
    "Cactus Comidas para llevar",
    "Centro comercial Moctezuma",
    "Chop-suey Chinese",
    "Comrcio Mineiro",
    "Consolidated Holdings",
    "Drachenblut Delikatessen",
    "Du monde entier",
    "Eastern Connection",
    "Ernst Handel",
    "Familia Arquibaldo"
  ];

  List<String> _shipperIDs = [
    "Speedy Express",
    "United Package",
    "Federal Shipping"
  ];

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: 'Order Page',
        theme: ThemeData(primarySwatch: Colors.blueGrey),
        home: Builder(
            builder: ( _formKey ) => Scaffold(
                appBar: AppBar(title: Text('Create an Order')),
                body: Center(
                    child: Container(
                        padding: EdgeInsets.fromLTRB(35.0, 50.0, 30.0, 30.0),
                        child: Column(children: <Widget>[
                          Container(
                            padding: EdgeInsets.fromLTRB(0.0, 50.0, 0.0, 0.0),
                            child: Column(
                              children: <Widget>[
                                OrderUI(),
                                RaisedButton(
                                  onPressed: () {},
                                  color: Colors.cyan,
                                  child: Text('Create New Order'),
                                ),
                              ],
                            ),
                          )
                        ]))))));
  }

  Widget OrderUI() {
    return Builder(
        builder: (context) => Column(
              children: <Widget>[
                Text("Customers"),
                DropdownButton(
                    onChanged: (value) {
                      setState(() {
                        _customers = value;
                      });
                    },
                    items: _customers.map((value) {
                      return DropdownMenuItem(child: Text(value), value: value);
                    }).toList()),
                Text("ShipperID"),
                DropdownButton(
                    onChanged: (value) {},
                    items: _shipperIDs.map((value) {
                      return DropdownMenuItem(child: Text(value), value: value);
                    }).toList()),
                Text("Order Date"),
                Text("Required Date"),
                Text("Freight")
              ],
            ));
  }
}
