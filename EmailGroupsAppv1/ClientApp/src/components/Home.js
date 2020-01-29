import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Welcome</h1>
        <p>This is proposed mail groups application.</p>
        <p>In <code>v1</code> there is no need to create an account.</p>
      </div>
    );
  }
}
