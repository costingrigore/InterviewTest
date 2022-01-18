import React, { Component } from 'react';

//Data structure that would be used to contain the table data from the API
const data = [
    { name: "Anom", age: 19, gender: "Male" },
    { name: "Megha", age: 19, gender: "Female" },
    { name: "Subham", age: 25, gender: "Male" },
]

//const sqlite3 = require('sqlite3').verbose();
//let db = new sqlite3.Database('./../../SqliteDB.db', (err) => {
//    if (err) {
//        console.error(err.message);
//    }
//    console.log('Connected to the chinook database.');
//});
//db.close();

export default class App extends Component {

  render () {
      return (
        //Table used to display the data from the API
        <table>
            <tr>
                <th>Name</th>
                <th>Age</th>
                <th>Gender</th>
            </tr>
            {data.map((val, key) => {
                return (
                    <tr key={key}>
                        <td>{val.name}</td>
                        <td>{val.age}</td>
                        <td>{val.gender}</td>
                    </tr>
                )
            })}
        </table>
    );
  }
}
