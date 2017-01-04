package nanogarch.creators;

interface ICreator<T>
{
	public function create():T;
}