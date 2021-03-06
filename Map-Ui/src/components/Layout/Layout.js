import { Fragment } from 'react';
import { Switch, Route, NavLink  } from 'react-router-dom';
import { Towns } from '../../pages/Towns/Towns';
import { Routes } from '../../pages/Routes/Routes';
import { LogisticCenters } from '../../pages/LogisticCenters/LogisticCenters';
import classes from './Layout.module.css';

export const Layout = () => {
  return (
   <Fragment>
    <header>
      <nav className={classes.nav}>
        <ul>
          <li><NavLink to="/">Towns</NavLink></li>
          <li><NavLink to="/routes">Routes</NavLink></li>
          <li><NavLink to="/logistic-centers">Logistic Centers</NavLink></li>
        </ul>
      </nav>
    </header>
    <main>
      <Switch>
        <Route path="/" exact component={Towns} />
        <Route path="/routes" exact component={Routes} />
        <Route path="/logistic-centers" exact component={LogisticCenters} />
      </Switch>
    </main>
   </Fragment>
  );
}
