import React, { useEffect } from "react";
import { useAuthContext } from "../AuthContext";
import { useHistory } from "react-router-dom";
import axios from "axios";
const Logout = () => {

    const { setUser } = useAuthContext();
    const history = useHistory();
    useEffect(() => {
        const logout = async () => {
            setUser(null);
            await axios.post('/api/account/logout');
            history.push('login');

        }
        logout();
    }, [])

    return (
        <h1>Logging out...</h1>
    )
}
export default Logout