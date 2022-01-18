import React, { Component } from 'react';

const numbers = [1, 2, 3, 5, 5];

const updatedNums = numbers.map((number) => {
    return <li>{number}</li>;
});

const sqlite3 = require('sqlite3').verbose();
let db = new sqlite3.Database('./SqliteDB.db', (err) => {
    if (err) {
        console.error(err.message);
    }
    console.log('Connected to the chinook database.');
});
db.close();

export default class App extends Component {

  render () {
    return (
        <div>Complete your app here</div>,
        <ul>
            {updatedNums}
        </ul >
    );
  }
}
