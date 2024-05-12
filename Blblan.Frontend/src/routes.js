import Home from "./pages/home.page";
import Login from "./pages/login.page";
import SignUp from "./pages/sign-up.page";
import { createBrowserRouter } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: '/sign-up',
    element: <SignUp />
  },
  {
    path: "/",
    element: <Home />,
  },
]);

export { router };
