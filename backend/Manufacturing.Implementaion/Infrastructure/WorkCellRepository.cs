namespace Manufacturing.Implementation.Infrastructure;

public class WorkCellRepository {

    public WorkCellRepository() { 
    
    }

    internal Task<WorkCellContext> GetById(int id) {
        throw new NotImplementedException();
    }

    public Task Save(WorkCellContext workCell) {
        throw new NotImplementedException();
    }

    public Task<WorkCellContext> Create(string alias, int productClass) {
        throw new NotImplementedException();
    }

    public Task Remove(int id) {
        throw new NotImplementedException();
    }

}
