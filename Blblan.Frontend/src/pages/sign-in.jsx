import { TextField } from "@mui/material";
import Button from "../components/atoms/Button";
import { useState } from "react";

export default function SignIn() {
  const [alreadyHaveAccount, setAlreadyHaveAccount] = useState(false);

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
          <TextField
            style={{ width: "100%" }}
            id="outlined-basic"
            label="Outlined"
            variant="outlined"
          />
          <div
            style={{
              margin: "24px 0 0",
            }}
          >
            <Button backgroundColor="#10a37f">Continue</Button>
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
            <div className="divider">
              <span className="divider-line" />
              <span className="divider-text">or</span>
              <span className="divider-line" />
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
              <Button color="#000" backgroundColor="#fff">
                <img
                  src="	https://auth.openai.com/assets/google-logo-NePEveMl.svg"
                  alt="ico"
                />
                <span>Continue with Google</span>
              </Button>
              <Button color="#000" backgroundColor="#fff">
                <img
                  src="	https://auth.openai.com/assets/microsoft-logo-BUXxQnXH.svg"
                  alt="ico"
                />
                <span>Continue with Microsoft Account</span>
              </Button>
              <Button color="#000" backgroundColor="#fff">
                <img
                  src="https://auth.openai.com/assets/apple-logo-tAoxPOUx.svg"
                  alt="ico"
                />
                <span>Continue with Apple</span>
              </Button>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
