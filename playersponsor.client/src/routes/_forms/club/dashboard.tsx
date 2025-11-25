import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_forms/club/dashboard')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_forms/club/dashboard"!</div>
}
