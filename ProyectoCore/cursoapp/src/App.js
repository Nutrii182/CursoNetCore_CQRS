import React from 'react';
import { ThemeProvider as MuithemeProvider} from "@material-ui/core/styles";
import theme from "./theme/theme";
import Login from './components/Seguridad/Login';

function App() {
  return (
    <MuithemeProvider theme={theme}>
      <Login/>
    </MuithemeProvider>
  );
}

export default App;
