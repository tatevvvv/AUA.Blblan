import { TextField, FormHelperText, FormControl } from "@mui/material";
import Button from "../components/atoms/Button";
import { useCallback, useState } from "react";
import { signUp } from "../api/auth/signUp"
import { logIn } from "../api/auth/logIn"
import { useMutation } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";

export default function SignIn() {
  const navigate = useNavigate()
  const [alreadyHaveAccount, setAlreadyHaveAccount] = useState(false);
  const requestLogIn = useMutation({ mutationFn: logIn, onSuccess: (data) => {
    console.log(data)
    localStorage.setItem('accessToken', data.token)
    navigate('/')
  } })
  const requestSignUp = useMutation({ mutationFn: signUp, onSuccess: () => {
    requestLogIn.mutate({ userName: formData.username, password: formData.password })
  } })
  const [formData, setFormData] = useState({
    email: "",
    username: "",
    password: "",
    confirmPassword: ""
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  const authenticateOrRegister = useCallback(() => {
    if (alreadyHaveAccount) {
      requestLogIn.mutate(formData)
    } else {
      if (!formData.email || !formData.password || !formData.confirmPassword) {
        return alert('One of the form fields is empty')
      }

      if (formData.confirmPassword !== formData.password) {
        return alert('Password confirmation is wrong')
      }

      requestSignUp.mutate(formData)
    }
  }, [alreadyHaveAccount, formData, requestSignUp])

  const handleSetAlreadyHaveAccount = () => {
    setAlreadyHaveAccount(!alreadyHaveAccount);
  };

  return (
    <div className="sign-in">
      <div className="oai-header">
        <img
          className="logo"
          src="https://auth.openai.com/assets/openai-logo-DmWoKcI3.svg"
          alt="ico"
        />
      </div>
      <main className="main-container">
        <h1 className="title">
          {!alreadyHaveAccount ? "Create an account" : "Welcome back"}
        </h1>
        <div className="login-container">
          <FormControl error={requestSignUp.isError} variant="standard" fullWidth>
            <TextField
              style={{ width: "100%", marginBottom: '10px' }}
              id="outlined-basic"
              label="Username"
              variant="outlined"
              name="username"
              type="text"
              value={formData.username}
              onChange={handleInputChange}
            />
            <TextField
              style={{ width: "100%", marginBottom: '10px' }}
              id="outlined-basic"
              label="Email"
              variant="outlined"
              name="email"
              type="email"
              value={formData.email}
              onChange={handleInputChange}
            />
            <TextField
              style={{ width: "100%", marginBottom: '10px' }}
              id="outlined-basic"
              label="Password"
              variant="outlined"
              type="password"
              name="password"
              value={formData.password}
              onChange={handleInputChange}
            />
            {
              !alreadyHaveAccount ?
              <TextField
                style={{ width: "100%" }}
                id="outlined-basic"
                label="Confirm Password"
                variant="outlined"
                type="password"
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleInputChange}
              /> : null
            }
            <FormHelperText>{requestSignUp.error?.message}</FormHelperText>
          </FormControl>
          <div
            style={{
              margin: "24px 0 0",
            }}
          >
            <Button backgroundColor="#10a37f" onClick={authenticateOrRegister}>
              {alreadyHaveAccount ? "Login" : "Sign up"}
            </Button>
            <div className="already-have-account">
              <span className="text">
                {!alreadyHaveAccount
                  ? "Already have an account?"
                  : "Don't have an account?"}
              </span>
              <span
                onClick={handleSetAlreadyHaveAccount}
                className="login-text"
              >
                {!alreadyHaveAccount ? "Login" : "Sign up"}
              </span>
            </div>
            <div
              className="sign-in-social"
              style={{
                display: "flex",
                flexDirection: "column",
                gap: "13px",
                alignItems: "center",
                marginTop: "15px",
                justifyContent: 'flex-start'
              }}
            >
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
