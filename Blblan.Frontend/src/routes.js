import Home from "./pages";
import SignIn from "./pages/sign-in";
import { createBrowserRouter } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: "/sign-in",
    element: <SignIn />,
  },
  {
    path: "/",
    element: <Home />,
  },
]);

export { router };
