import { FormControl } from "@mui/material";
import Button from "../components/atoms/Button";
import { logIn } from "../api/auth/logIn"
import { useMutation } from "@tanstack/react-query";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { TextInput } from "../components/atoms/TextInput";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from 'yup'

const loginSchema = yup.object({
  username: yup.string().required(),
  password: yup.string().required()
})

export default function Login() {
  const navigate = useNavigate()
  const requestLogIn = useMutation({
    mutationFn: logIn, onSuccess: (data) => {
      localStorage.setItem('accessToken', data.token)
      navigate('/')
    }
  })

  const { control, handleSubmit } = useForm({
    defaultValues: {
      username: '',
      password: ''
    },
    resolver: yupResolver(loginSchema)
  })

  const onSubmit = (formData) => {
    requestLogIn.mutate(formData)
  }

  return (
    <div className="auth">
      <div className="oai-header">
        <img
          className="logo"
          src="https://auth.openai.com/assets/openai-logo-DmWoKcI3.svg"
          alt="ico"
        />
      </div>
      <main className="main-container">
        <h1 className="title">
          Welcome back
        </h1>
        <div className="auth-container">
          <form onSubmit={handleSubmit(onSubmit)}>
            <FormControl variant="standard" fullWidth>
              <TextInput
                style={{ width: "100%", marginBottom: '10px' }}
                id="outlined-basic"
                label="Username"
                variant="outlined"
                name="username"
                type="text"
                control={control}
              />
              <TextInput
                style={{ width: "100%", marginBottom: '10px' }}
                id="outlined-basic"
                label="Password"
                variant="outlined"
                name="password"
                type="password"
                control={control}
              />
            </FormControl>
            <div
              style={{ // remove
                margin: "24px 0 0",
              }}
            >
              <Button backgroundColor="#10a37f" type='submit'>
                Login
              </Button>
              <div className="already-have-account">
                <span className="text">
                  Don't have an account?
                </span>
                <Link to='/sign-up'>
                  <span
                    className="login-text"
                  >
                    Sign up
                  </span>
                </Link>
              </div>
            </div>
          </form>
        </div>
      </main>
    </div>
  );
}
