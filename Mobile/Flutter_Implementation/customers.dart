import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;
import 'globals.dart';

Future< CustomersList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/customers');
  
  if (response.statusCode == 200)
  {
    return CustomersList.fromJson( json.decode( response.body ) );
  } 
  else 
  {
    throw Exception( 'Failed to load post' );
  }
}

class CustomersList 
{
  final List< CustomersPost > posts;

  CustomersList( { this.posts } );

  factory CustomersList.fromJson( List< dynamic > parsedJson )
  {
    List< CustomersPost > customers = new List< CustomersPost >();
    customers = parsedJson.map( ( i ) => CustomersPost.fromJson( i ) ).toList();

    return CustomersList( posts: customers );
  }
}

class CustomersPost
{
  final String customerID;
  final String companyName;
  final String contactName;
  final String contactTitle;
  final String address;
  final String city;
  final String region;
  final String postalCode;
  final String country;
  final String phone;
  final String fax;

  CustomersPost( 
    { 
      this.customerID, 
      this.companyName,
      this.contactName,
      this.contactTitle,
      this.address,
      this.city,
      this.region,
      this.postalCode,
      this.country,
      this.phone,
      this.fax 
    } );

  factory CustomersPost.fromJson (Map< String, dynamic > json )
  {
    return CustomersPost(
      customerID:    json['CustomerID'],
      companyName:   json['CompanyName'],
      contactName:   json[ 'ContactName' ],
      contactTitle:  json[ 'ContactTitle' ],
      address:       json[ 'Address' ],
      city:          json[ 'City' ],
      region:        json[ 'Region' ],
      postalCode:    json[ 'PostalCode' ],
      country:       json[ 'Country' ],
      phone:         json[ 'Phone' ],
      fax:           json[ 'Fax' ]
    );
  }
}

class CustomersWidg extends StatefulWidget 
{
  CustomersWidg({Key key}) : super(key: key);
  @override
  _CustomersWidgState createState() => new _CustomersWidgState();
}

class _CustomersWidgState extends State< CustomersWidg > 
{
  Widget appBarTitle = new Text(
    "Search For Customers",
    style: Globals.textStyle,
  );
  Icon actionIcon = new Icon(
    Icons.search,
    color: Colors.white,
  );

  final key = new GlobalKey<ScaffoldState>();
  final TextEditingController _searchQuery = new TextEditingController();
  List< CustomersPost > _list;
  bool _IsSearching;
  String _searchText = "";

  _SearchListState() {
    _searchQuery.addListener(() {
      if (_searchQuery.text.isEmpty) {
        setState(() {
          _IsSearching = false;
          _searchText = "";
        });
      } else {
        setState(() {
          _IsSearching = true;
          _searchText = _searchQuery.text;
        });
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: Globals.backgroundColor,
      key: key,
      appBar: AppBar(
        centerTitle: true,
        title: Text( 'Customers', style: Globals.textStyle ), 
      ),
      body: FutureBuilder< CustomersList >(
        future: fetchPost(),
        builder: ( context, snapshot ) {
          if ( snapshot.hasData )
          {
            _list = List();

            CustomersPost post;

            for ( int i = 0; i < snapshot.data.posts.length; i++ )
            {
              String customerID   = snapshot.data.posts[ i ].customerID;
              String companyName  = snapshot.data.posts[ i ].companyName.toString();
              String contact      = snapshot.data.posts[ i ].contactName;
              String title        = snapshot.data.posts[ i ].contactTitle;
              String address      = snapshot.data.posts[ i ].address;
              String city         = snapshot.data.posts[ i ].city;
              String region       = snapshot.data.posts[ i ].region;
              String postalCode   = snapshot.data.posts[ i ].postalCode;
              String country      = snapshot.data.posts[ i ].country;
              String phone        = snapshot.data.posts[ i ].phone;
              String fax          = snapshot.data.posts[ i ].fax;

              post = CustomersPost(
                customerID: customerID,
                companyName: companyName,
                contactName: contact,
                contactTitle: title,
                address: address,
                city: city,
                region: region,
                postalCode: postalCode,
                country: country,
                phone: phone,
                fax: fax
              );

              _list.add( post );
            }

            return ListView.builder(
              itemCount: _list.length,
              itemBuilder: ( context, index ) {
                return Card(
                  elevation: 8.0,
                  margin: EdgeInsets.symmetric( horizontal: 10.0, vertical: 10.0 ),
                  child: Container(
                    decoration: BoxDecoration( color: Globals.barColor ),
                    child: ExpansionTile(
                      //contentPadding: EdgeInsets.symmetric( horizontal: 10.0, vertical: 6.0 ),
                      title: Text( 
                        _list[ index ].companyName, 
                        style: Globals.textStyle, 
                        textAlign: TextAlign.center 
                      ),
                      trailing: Icon( Icons.arrow_drop_down ),
                      children: <Widget>[
                        Padding(
                          padding: EdgeInsets.only( left: 30.0, bottom: 20.0 ),
                          child: Row(
                            children: <Widget>[
                              Text(
                                'Contact Name:   ' + '\n' +
                                'Title:          ' + '\n' +
                                'Address:        ' + '\n' +
                                'City:           ' + '\n' +
                                'Region:         ' + '\n' +
                                'Postal Code:    ' + '\n' +
                                'Country:        ' + '\n' +
                                'Phone:          ' + '\n' +
                                'Fax:            ',
                                style: Globals.textStyle
                              ),
                              Text( 
                                _list[ index ].contactName + '\n' +
                                _list[ index ].contactTitle + '\n' +
                                _list[ index ].address + '\n' +
                                _list[ index ].city + '\n' +
                                _list[ index ].region + '\n' +
                                _list[ index ].postalCode + '\n' +
                                _list[ index ].country + '\n' +
                                _list[ index ].phone + '\n' +
                                _list[ index ].fax,
                                style: Globals.textStyle
                              )
                            ],
                          ),
                        )
                      ],
                    ),
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

//   List<ChildItem> _buildList() {
//     return _list.map((contact) => new ChildItem(contact)).toList();
//   }

//   List<ChildItem> _buildSearchList() {
//     if (_searchText.isEmpty) {
//       return _list.map((contact) => new ChildItem(contact)).toList();
//     } else {
//       List<String> _searchList = List();
//       for (int i = 0; i < _list.length; i++) {
//         String name = _list.elementAt(i);
//         if (name.toLowerCase().contains(_searchText.toLowerCase())) {
//           _searchList.add(name);
//         }
//       }
//       return _searchList.map((contact) => new ChildItem(contact)).toList();
//     }
//   }

//   Widget buildBar(BuildContext context) {
//     return new AppBar(centerTitle: true, title: appBarTitle, actions: <Widget>[
//       new IconButton(
//         icon: actionIcon,
//         onPressed: () {
//           setState(() {
//             if (this.actionIcon.icon == Icons.search) {
//               this.actionIcon = new Icon(
//                 Icons.close,
//                 color: Colors.white,
//               );
//               this.appBarTitle = new TextField(
//                 controller: _searchQuery,
//                 style: new TextStyle(
//                   color: Colors.white,
//                 ),
//                 decoration: new InputDecoration(
//                     prefixIcon: new Icon(Icons.search, color: Colors.white),
//                     hintText: "Search...",
//                     hintStyle: new TextStyle(color: Colors.white)),
//               );
//               _handleSearchStart();
//             } else {
//               _handleSearchEnd();
//             }
//           });
//         },
//       ),
//     ]);
//   }

//   void _handleSearchStart() {
//     setState(() {
//       _IsSearching = true;
//     });
//   }

//   void _handleSearchEnd() {
//     setState(() {
//       this.actionIcon = new Icon(
//         Icons.search,
//         color: Colors.white,
//       );
//       this.appBarTitle = new Text(
//         "Search Sample",
//         style: new TextStyle(color: Colors.white),
//       );
//       _IsSearching = false;
//       _searchQuery.clear();
//     });
//   }
// }

// class ChildItem extends StatelessWidget {
//   final String name;
//   ChildItem(this.name);
//   @override
//   Widget build(BuildContext context) {
//     return new ListTile(title: new Text(this.name));
//   }
}