import React, { useState } from 'react';
import axios from 'axios';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

const handleSubmit = async (e) => {
    e.preventDefault();

    try {
        const response = await axios.post('http://localhost:5198/api/auth/login', {
            username,
            password
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        });

        console.log('Login successful:', response.data);
        // Add logic for handling successful login (e.g., redirecting to another page)
    } catch (error) {
        console.error('Login failed:', error);
        // Add logic for handling failed login (e.g., displaying error message)
    }
};


    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Username/Email" value={username} onChange={(e) => setUsername(e.target.value)} />
                <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                <button type="submit">Login</button>
            </form>
        </div>
    );
};

export default Login;
