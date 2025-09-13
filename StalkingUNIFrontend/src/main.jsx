import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import './index.css'

import PageSearch from './pages/PageSearch.jsx'
import PageStudent from './pages/PageStudent.jsx'

const router = createBrowserRouter([
  { path: '/', element: <PageSearch /> },
  { path: '/user/:matricola', element: <PageStudent /> },
  { path: '*', element: <PageSearch /> }
],
  { basename: import.meta.env.BASE_URL }
)

ReactDOM.createRoot(document.getElementById('root')).render(
  <RouterProvider router={router} />
)