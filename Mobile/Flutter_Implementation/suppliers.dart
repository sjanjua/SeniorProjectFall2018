import 'package:flutter/material.dart';
import 'home.dart';
import 'signup.dart';
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart' show rootBundle;
import 'globals.dart';

//********************************************************************
//suppliers.dart adds the functionality to see suppliers by 
//Fetch post to get supplier information from the JSON 
//pulls the data from the supplier endpoint and feeds it to the app
//shippers option is accessible from the homepage 
//*********************************************************************
Future< SuppliersList > fetchPost() async 
{
  final response = await http.get('http://inv.azurewebsites.net/api/data/suppliers');

  if (response.statusCode == 200)
  {
    return SuppliersList.fromJson( json.decode( response.body ) );
  } 
  else 
  {
    throw Exception( 'Failed to load post' );
  }
}

//Class for a suppliers list that parses the JSON post from the endpoint 
class SuppliersList 
{
  final List< SuppliersPost > posts;

  SuppliersList( { this.posts } );

  factory SuppliersList.fromJson( List< dynamic > parsedJson )
  {
    List< SuppliersPost > suppliers = new List< SuppliersPost >();
    suppliers = parsedJson.map( ( i ) => SuppliersPost.fromJson( i ) ).toList();

    return SuppliersList( posts: suppliers );
  }
}

//Making the class for supplier post that takes the parsed JSON and puts the JSON variables 
//into the local variables inside the app
class SuppliersPost
{
  final int    supplierID;
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

  SuppliersPost( 
    { 
      this.supplierID, 
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

  factory SuppliersPost.fromJson (Map< String, dynamic > json )
  {
    return SuppliersPost(
      supplierID:    json[ 'SupplierID' ],
      companyName:   json[ 'CompanyName' ],
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

//Supplierswidget instantiation 
class SuppliersWidg extends StatefulWidget 
{
  SuppliersWidg({Key key}) : super(key: key);
  @override
  _SuppliersWidgState createState() => new _SuppliersWidgState();
}


class _SuppliersWidgState extends State< SuppliersWidg > 
{
  Widget appBarTitle = new Text(
    "Suppliers",
    style: Globals.textStyle,
  );
  Icon actionIcon = new Icon(
    Icons.search,
    color: Colors.white,
  );

  final key = new GlobalKey<ScaffoldState>();
  final TextEditingController _searchQuery = new TextEditingController();
  List< SuppliersPost > _list;
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

  //building the widget for the user interface
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: Globals.backgroundColor,
      key: key,
      appBar: AppBar(
            centerTitle: true,
            title: Text( 'Suppliers', style: Globals.textStyle ), 
          ),
      body: FutureBuilder< SuppliersList >(
        future: fetchPost(),
        builder: ( context, snapshot ) {
          if ( snapshot.hasData )
          {
            _list = List();

            SuppliersPost post;

            for ( int i = 0; i < snapshot.data.posts.length; i++ )
            {
              int    supplierID      = snapshot.data.posts[ i ].supplierID;
              String supplierName    = snapshot.data.posts[ i ].companyName.toString();
              String supplierContact = snapshot.data.posts[ i ].contactName;
              String title           = snapshot.data.posts[ i ].contactTitle;
              String address         = snapshot.data.posts[ i ].address;
              String city            = snapshot.data.posts[ i ].city;
              String region          = snapshot.data.posts[ i ].region;
              String postalCode      = snapshot.data.posts[ i ].postalCode;
              String country         = snapshot.data.posts[ i ].country;
              String phone           = snapshot.data.posts[ i ].phone;
              String fax             = snapshot.data.posts[ i ].fax;

              post = SuppliersPost(
                supplierID: supplierID,
                companyName: supplierName,
                contactName: supplierContact,
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
                                _list[ index ].contactName  + '\n' +
                                _list[ index ].contactTitle + '\n' +
                                _list[ index ].address      + '\n' +
                                _list[ index ].city         + '\n' +
                                _list[ index ].region       + '\n' +
                                _list[ index ].postalCode   + '\n' +
                                _list[ index ].country      + '\n' +
                                _list[ index ].phone        + '\n' +
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
