import "./App.css";
import { ApiProvider } from "./api";
import { router } from "./routes";
import { RouterProvider } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <ApiProvider>
        <RouterProvider router={router} />
      </ApiProvider>
    </div>
  );
}

export default App;
