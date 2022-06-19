import React from 'react';
import { Link } from 'react-router-dom';
import { useAuthContext } from '../AuthContext';
const Layout = (props) => {
    const { user } = useAuthContext();

    return (
        <div>
            <header>
                <nav className="navbar navbar-expand-sm navbar-dark fixed-top bg-dark border-bottom box-shadow">
                    <div className="container">
                        <Link to='/'>Tasks
                        </Link>

                        <button className="navbar-toggler" type="button" data-toggle="collapse"
                            data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                            <ul className="navbar-nav flex-grow-1">

                                <li className='nav-item'>
                                    <Link to='/'>
                                        <span className='nav-link text-light'>
                                            Home
                                        </span>
                                    </Link>
                                </li>
                                {!user && <li className="nav-item">
                                    <Link to='/signup' className='nav-link text-light'>
                                        Signup
                                    </Link>
                                </li>}

                                {!user && <li className='nav-item'>
                                    <Link to='/login'>
                                        <span className='nav-link text-light'>
                                            Login
                                        </span>
                                    </Link>
                                </li>
                                }
                                {user && <li className='nav-item'>
                                    <Link to='/logout'>
                                        <span className='nav-link text-light'>
                                            Logout
                                        </span>
                                    </Link>
                                </li>
                                }


                            </ul>
                        </div>
                    </div>
                </nav>
            </header>

            <div className="container" style={{ marginTop: 80 }}>
                <main role="main" className="pb-3">
                    {props.children}
                </main>

            </div>

        </div>
    )
}

export default Layout;