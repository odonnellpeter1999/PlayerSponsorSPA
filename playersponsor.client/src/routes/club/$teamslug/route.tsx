import { createFileRoute, Outlet } from '@tanstack/react-router'

export const Route = createFileRoute('/club/$teamslug')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <>
      <Outlet/>
    </>
  )
}
