import React from 'react';
import { Route } from 'react-router-dom';
import Layout from './Components/Layout';
import { AuthContextComponent } from './AuthContext';
import Signup from './Pages/Signup';
import Login from './Pages/Login';
import Home from './Pages/Home';
import PrivateRoute from './Components/PrivateRoute';
import Logout from './Pages/Logout';



const App = () => {
    return (
        <AuthContextComponent>
            <Layout>
                <PrivateRoute exact path='/' component={Home} />
                <Route exact path='/signup' component={Signup} />
                <Route exact path='/login' component={Login} />
                <PrivateRoute exact path='/logout' component={Logout} />

            </Layout>
        </AuthContextComponent>




    )
}
export default App;