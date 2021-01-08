import React from 'react';
import ReactDOM from 'react-dom';
import ApolloClient from 'apollo-boost';
import { ApolloProvider } from 'react-apollo';
import { ApolloProvider as ApolloHooksProvider } from 'react-apollo-hooks';
import './index.css';
import App from './App';
import { BrowserRouter as Router,Route, Switch} from 'react-router-dom';

import Login from "./components/LoginForm/Login";

const client = new ApolloClient({
  uri: 'https://spacexdata.herokuapp.com/graphql',
});

ReactDOM.render(
  <ApolloProvider client={client}>
    <ApolloHooksProvider client={client}>
      <Router>
          <Switch>
              <Route path="/dashboard" component={App} />
              <Route path="/login" component={Login} />
          </Switch>
        </Router>
    </ApolloHooksProvider>
  </ApolloProvider>,
  document.getElementById('root'),
);
