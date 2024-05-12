import { FormControl } from "@mui/material";
import Button from "../components/atoms/Button";
import { signUp } from "../api/auth/signUp"
import { useMutation } from "@tanstack/react-query";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { TextInput } from "../components/atoms/TextInput";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from 'yup'

const signUpSchema = yup.object({
  username: yup.string().required(),
  email: yup.string().email().required(),
  password: yup.string().min(6).matches(/(?=.*[a-z])/g, { message: 'Should contain at least one lowercase letter'}).required(),
  confirmPassword: yup.string()
    .oneOf([yup.ref('password'), null], 'Passwords must match')
})

export default function SignUp() {
  const navigate = useNavigate()
  const requestSignUp = useMutation({
    mutationFn: signUp, onSuccess: (data) => {
      localStorage.setItem('accessToken', data.token)
      navigate('/login')
    }
  })

  const { control, handleSubmit } = useForm({
    defaultValues: {
      username: '',
      password: '',
      confirmPassword: ''
    },
    resolver: yupResolver(signUpSchema)
  })

  const onSubmit = (formData) => {
    requestSignUp.mutate(formData)
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
      <h1 className="title">
        Join us
    </h1>
      <main className="main-container">
        <h1 className="title">
          
        </h1>
        <div className="auth-container">
          <form onSubmit={handleSubmit(onSubmit)}>
            <FormControl variant="standard" fullWidth>
            <TextInput
                style={{ width: "100%", marginBottom: '10px' }}
                label="Username"
                variant="outlined"
                name="username"
                type="text"
                control={control}
            />
            <TextInput
                style={{ width: "100%", marginBottom: '10px' }}
                label="Email"
                variant="outlined"
                name="email"
                type="text"
                control={control}
            />
              <TextInput
                style={{ width: "100%", marginBottom: '10px' }}
                label="Password"
                variant="outlined"
                name="password"
                type="password"
                control={control}
              />
              <TextInput
                style={{ width: "100%", marginBottom: '10px' }}
                label="Confirm password"
                variant="outlined"
                name="confirmPassword"
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
                Sign up
              </Button>
              <div className="already-have-account">
                <span className="text">
                  Already have an account?
                </span>
                <Link to='/login'>
                  <span
                    className="login-text"
                  >
                    Login
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
